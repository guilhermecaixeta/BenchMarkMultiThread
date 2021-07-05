using System;
using System.IO;
using System.Runtime.InteropServices;

namespace BenchMarkMultiThread.File
{
    public static class FileManager
    {
        private static string MainPath = Directory.CreateDirectory("files").FullName;

        public static string GetFilePath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                //MainPath = "/src/files";
            }
            var date = DateTime.Now.ToString("ddMMyyyyhhmmss");
            return Path.Combine(MainPath, date);
        }

        public static void CreateIfNotExist(string path)
        {
            if (!Directory.Exists(path))
            {
                _ = Directory.CreateDirectory(path);
            }
        }

        public static string ConcatFilePath(string filePath, string fileName) =>
            Path.Combine(filePath, $"{fileName}.txt");
    }
}
