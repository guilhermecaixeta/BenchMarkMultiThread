using BenchMarkMultiThread.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BenchMarkMultiThread.Streamers
{
    public static class BufferStreamer
    {
        private static readonly UnicodeEncoding UnicodeEncoding = new();
        private const int BUFFER = 4096;
        private const FileShare SHARE = FileShare.ReadWrite;
        private const FileMode MODE = FileMode.OpenOrCreate;
        private const FileAccess ACCESS = FileAccess.ReadWrite;
        private const FileOptions OPTIONS = FileOptions.Asynchronous;

        public static async Task WriteStreamAsync(KeyValuePair<string, string> file, string path)
        {
            var filePath = FileManager.ConcatFilePath(path, file.Key);
            var result = UnicodeEncoding.GetBytes(file.Value);

            using var stream = new FileStream(filePath, MODE, ACCESS, SHARE, BUFFER, OPTIONS);
            using var bufferedStream = new BufferedStream(stream, result.Length);
            bufferedStream.Seek(0, SeekOrigin.End);
            await bufferedStream.WriteAsync(result.AsMemory(0, result.Length));
        }

        public static (BufferedStream stream, Task task) WriteStream(KeyValuePair<string, string> file, string path)
        {
            var filePath = FileManager.ConcatFilePath(path, file.Key);
            var result = UnicodeEncoding.GetBytes(file.Value);

            var stream = new FileStream(filePath, MODE, ACCESS, SHARE, BUFFER, OPTIONS);
            var bufferedStream = new BufferedStream(stream, result.Length);
            bufferedStream.Seek(0, SeekOrigin.End);
            var task = stream.WriteAsync(result, 0, result.Length);
            return (bufferedStream, task);
        }
    }
}
