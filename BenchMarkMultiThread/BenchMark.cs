using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchMarkMultiThread
{
    [EvaluateOverhead]
    [MemoryDiagnoser]
    [ThreadingDiagnoser]
    [MarkdownExporter, HtmlExporter]
    //[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
    [SimpleJob(RunStrategy.Monitoring, RuntimeMoniker.NetCoreApp50)]
    public class BenchMark
    {
        public const string CHARS = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789/*-+. !@#$%¨&*()_";
        public const int TEXT_SIZE = 50;
        public readonly Random Random = new Random();
        public UnicodeEncoding UnicodeEncoding = new UnicodeEncoding();
        public string MainPath = Directory.CreateDirectory("files").FullName;
        public string FilesPath;
        public Dictionary<string, string> Dictionary;

        [Params(1_000, 3_000, 5_000)]
        public int iterations;

        [IterationSetup]
        public void Setup()
        {
            Dictionary = new Dictionary<string, string>(iterations);

            FilesPath = Path.Combine(MainPath, DateTime.Now.ToString("ddMMyyyyhhmmss"));

            CreateIfNotExist();

            FulfilDictionary();
        }

        [IterationCleanup]
        public void TearDown()
        {
            Dictionary.Clear();
        }

        [Benchmark]
        public void SaveFilesParalleling_ForceParallelism_AutoBufferedAsync_AutoDisposed() =>
            RunWritingWithOptions(ParallelExecutionMode.ForceParallelism, ParallelMergeOptions.AutoBuffered);

        [Benchmark]
        public void SaveFilesParalleling_ForceParallelism_AutoBufferedAsync_HandledDisposed() =>
            Dictionary
               .AsParallel()
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                .WithMergeOptions(ParallelMergeOptions.AutoBuffered)
                .ForAll(async file =>
                {
                    var (stream, task) = WriteFileAsStream(file);
                    await task;
                    stream.Close();
                });

        [Benchmark]
        public void SaveFilesParalleling_ForEachAsync_AutoDisposed() =>
            Parallel.ForEach(Dictionary, async file =>
                {
                    await WriteFileAsStreamAsync(file);
                });

        [Benchmark]
        public void SaveFilesParalleling_ForEachAsync_HandledDisposed() =>
            Parallel.ForEach(Dictionary, async file =>
                {
                    var (stream, task) = WriteFileAsStream(file);
                    await task;
                    stream.Close();
                });

        [Benchmark]
        public async Task SaveFiles_Async()
        {
            var streams = new List<FileStream>();
            var tasks = new List<Task>();

            Dictionary
               .ToList()
               .ForEach(file =>
                   {
                       var result = WriteFileAsStream(file);

                       tasks.Add(result.task);
                       streams.Add(result.stream);
                   });

            await Task.WhenAll(tasks);
            streams
               .ToList()
               .ForEach(stream => stream.Close());
        }

        private void CreateIfNotExist()
        {
            if (!Directory.Exists(FilesPath))
            {
                _ = Directory.CreateDirectory(FilesPath);
            }
        }

        private void RunWritingWithOptions(ParallelExecutionMode executionMode, ParallelMergeOptions mergeOptions) =>
            Dictionary
                .AsParallel()
                .WithExecutionMode(executionMode)
                .WithMergeOptions(mergeOptions)
                .ForAll(async file => await WriteFileAsStreamAsync(file));

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

        private async Task WriteFileAsStreamAsync(KeyValuePair<string, string> file)
        {
            CreateFileStream(file, out byte[] result, out FileStream stream);
            await stream.WriteAsync(result.AsMemory(0, result.Length));
            stream.Close();
        }

        private (FileStream stream, Task task) WriteFileAsStream(KeyValuePair<string, string> file)
        {
            CreateFileStream(file, out byte[] result, out FileStream stream);
            var task = stream.WriteAsync(result, 0, result.Length);
            return (stream, task);
        }

        private void CreateFileStream(KeyValuePair<string, string> file, out byte[] result, out FileStream stream)
        {
            string filePath = GetFilePath(file.Key);
            result = UnicodeEncoding.GetBytes(file.Value);
            var buffer = result.Length;
            var share = FileShare.ReadWrite;
            var mode = FileMode.OpenOrCreate;
            var access = FileAccess.ReadWrite;
            var options = FileOptions.Asynchronous;

            stream = new FileStream(filePath, mode, access, share, buffer, options);
        }

        private string GetFilePath(string fileName) =>
            System.IO.Path.Combine(FilesPath, $"{fileName}.txt");
    }
}