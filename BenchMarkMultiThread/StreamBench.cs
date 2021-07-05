using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchMarkMultiThread.File;
using BenchMarkMultiThread.Streamers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BenchMarkMultiThread
{
    [EvaluateOverhead]
    [MemoryDiagnoser]
    [ThreadingDiagnoser]
    [MarkdownExporter, HtmlExporter]
    [SimpleJob(RunStrategy.Monitoring, RuntimeMoniker.NetCoreApp50)]
    public class StreamBench
    {
        public const string CHARS = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789/*-+. !@#$%¨&*()_";
        public readonly Random Random = new Random();
        public const int TEXT_SIZE = 100;

        public string FilesPath;
        public Dictionary<string, string> Dictionary;

        [Params(10_000, 20_000, 25_000)]
        public int iterations;

        [IterationSetup]
        public void Setup()
        {
            Dictionary = new Dictionary<string, string>(iterations);
            
            FilesPath = FileManager.GetFilePath();
            FileManager.CreateIfNotExist(FilesPath);

            FulfilDictionary();
        }

        //[Benchmark]
        public void WriteFileStream_AsParallel_AutoBuffered_ImplicityDisposed() =>
            RunAsParallel(ParallelExecutionMode.ForceParallelism, ParallelMergeOptions.AutoBuffered, async file => 
                await FileStreamer.WriteStreamAsync(file, FilesPath));

        //[Benchmark]
        public void WriteFileStream_AsParallel_AutoBuffered_ExplicitDisposed() =>
            RunAsParallel(ParallelExecutionMode.ForceParallelism, ParallelMergeOptions.AutoBuffered, async file =>
                {
                    var (stream, task) = FileStreamer.WriteStream(file, FilesPath);
                    await task;
                    stream.Close();
                });

        [Benchmark]
        public void WriteMemoryStream_AsParallel_AutoBuffered_ImplicityDisposed() =>
            RunAsParallel(ParallelExecutionMode.ForceParallelism, ParallelMergeOptions.AutoBuffered, async file =>
                await MemoryStreamer.WriteStreamAsync(file, FilesPath));

        //[Benchmark]
        public void WriteMemoryStream_AsParallel_AutoBuffered_ExplicitDisposed() =>
            RunAsParallel(ParallelExecutionMode.ForceParallelism, ParallelMergeOptions.AutoBuffered, async file =>
                {
                    var (stream, task) = MemoryStreamer.WriteStream(file, FilesPath);
                    await task;
                    stream.Close();
                });

        //[Benchmark]
        public void WriteBufferedStream_AsParallel_AutoBuffered_ImplicityDisposed() =>
            RunAsParallel(ParallelExecutionMode.ForceParallelism, ParallelMergeOptions.AutoBuffered, async file => 
                await BufferStreamer.WriteStreamAsync(file, FilesPath));

        //[Benchmark]
        public void WriteBufferedStream_AsParallel_AutoBuffered_ExplicitDisposed() =>
            RunAsParallel(ParallelExecutionMode.ForceParallelism, ParallelMergeOptions.AutoBuffered, async file =>
                {
                    var (stream, task) = BufferStreamer.WriteStream(file, FilesPath);
                    await task;
                    stream.Close();
                });

        [Benchmark]
        public void WriteFileStream_ParallelForEach_ImplicityDisposed() =>
            Parallel.ForEach(Dictionary, async file =>
                {
                    await BufferStreamer.WriteStreamAsync(file, FilesPath);
                });

        [Benchmark]
        public void WriteFileStream_ParallelInvoke_ImplicityDisposed() =>
            Parallel.Invoke(Dictionary.Select(file => {
                Action action = async () =>
                {
                    await BufferStreamer.WriteStreamAsync(file, FilesPath);
                };
                return action;
            }).ToArray());

        [Benchmark]
        public void WriteMemoryStream_ParallelInvoke_ImplicityDisposed() =>
            Parallel.Invoke(Dictionary.Select(file => {
                Action action = async () =>
                {
                    await MemoryStreamer.WriteStreamAsync(file, FilesPath);
                };
                return action;
            }).ToArray());

        [Benchmark]
        public void WriteMemoryStream_ParallelForEach_ExplicityDisposed() =>
            Parallel.ForEach(Dictionary, async file =>
                {
                    var (stream, task) = MemoryStreamer.WriteStream(file, FilesPath);
                    await task;
                    stream.Close();
                });

        //[Benchmark]
        public async Task WriteBufferedStream_Async()
        {
            var streams = new List<BufferedStream>();
            var tasks = new List<Task>();

            Dictionary
               .ToList()
               .ForEach(file =>
                   {
                       var result = BufferStreamer.WriteStream(file, FilesPath);

                       tasks.Add(result.task);
                       streams.Add(result.stream);
                   });

            await Task.WhenAll(tasks);
            streams
               .ToList()
               .ForEach(stream => stream.Close());
        }

        //[Benchmark]
        public async Task WriteFileStream_Async()
        {
            var streams = new List<FileStream>();
            var tasks = new List<Task>();

            Dictionary
               .ToList()
               .ForEach(file =>
               {
                   var result = FileStreamer.WriteStream(file, FilesPath);

                   tasks.Add(result.task);
                   streams.Add(result.stream);
               });

            await Task.WhenAll(tasks);
            streams
               .ToList()
               .ForEach(stream => stream.Close());
        }

        private string GetText() =>
            new(Enumerable.Repeat(CHARS, TEXT_SIZE).Select(s => s[Random.Next(s.Length)]).ToArray());

        private void FulfilDictionary() =>
            Enumerable
                .Range(0, iterations)
                .ToList()
                .ForEach(i =>
                    {
                        var text = GetText();
                        var fileName = GetValidFileName();
                        Dictionary.Add(fileName, text);
                    });

        private string GetValidFileName()
        {
            string fileName;

            {
                fileName = Guid.NewGuid().ToString();
            } while (Dictionary.ContainsKey(fileName)) ;

            return fileName;
        }

        private void RunAsParallel(ParallelExecutionMode executionMode, ParallelMergeOptions mergeOptions, Action<KeyValuePair<string, string>> action) =>
            Dictionary
                .AsParallel()
                .WithExecutionMode(executionMode)
                .WithMergeOptions(mergeOptions)
                .ForAll(action);
    }
}