using BenchmarkDotNet.Running;

namespace BenchMarkMultiThread
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}