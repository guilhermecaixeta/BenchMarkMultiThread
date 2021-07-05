using BenchMarkMultiThread.File;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BenchMarkMultiThread.Streamers
{
    public static class MemoryStreamer
    {
        private static readonly UnicodeEncoding UnicodeEncoding = new();
        private const int BUFFER = 4096;
        private const FileShare SHARE = FileShare.ReadWrite;
        private const FileMode MODE = FileMode.OpenOrCreate;
        private const FileAccess ACCESS = FileAccess.ReadWrite;
        private const FileOptions OPTIONS = FileOptions.Asynchronous;

        public static async Task WriteStreamAsync(KeyValuePair<string, string> file, string path)
        {
            string filePath = FileManager.ConcatFilePath(path, file.Key);
            var result = UnicodeEncoding.GetBytes(file.Value);

            using var memoryStream = new MemoryStream(result);
            using var stream = new FileStream(filePath, MODE, ACCESS, SHARE, BUFFER, OPTIONS);

            memoryStream.Position = 0;
            memoryStream.Seek(0, SeekOrigin.End);
            await memoryStream.CopyToAsync(stream);
        }

        public static (FileStream stream, Task task) WriteStream(KeyValuePair<string, string> file, string path)
        {
            string filePath = FileManager.ConcatFilePath(path, file.Key);
            var result = UnicodeEncoding.GetBytes(file.Value);

            var memoryStream = new MemoryStream(result);
            var stream = new FileStream(filePath, MODE, ACCESS, SHARE, BUFFER, OPTIONS);

            memoryStream.Position = 0;
            memoryStream.Seek(0, SeekOrigin.End);
            var task = memoryStream.CopyToAsync(stream);

            return (stream, task);
        }
    }
}
