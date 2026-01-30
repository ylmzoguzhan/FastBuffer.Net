using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using FastBuffer;
using System.Buffers;
using System.IO;
namespace ConsoleApp
{
    [MemoryDiagnoser] 
    [RankColumn]
    public class BufferBenchmarks
    {
        private const string TestString = "Performance_Is_A_Feature";
        private readonly MemoryStream _nullStream = new MemoryStream(2048);

        [Benchmark(Baseline = true)]
        public void Standard_BinaryWriter()
        {
            using var ms = new MemoryStream(1024);
            using var writer = new BinaryWriter(ms);

            writer.Write(101);
            writer.Write(TestString);

            var data = ms.ToArray();
        }

        [Benchmark]
        public void FastBuffer_ZeroAlloc()
        {

            using var scope = new FastBufferScope(1024);

            scope.WriteInt32(101);
            scope.WriteRawString(TestString);

            var data = scope.WrittenSpan;
        }
    }
}
