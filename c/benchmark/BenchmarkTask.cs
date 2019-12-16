using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
	{
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            var watch = new Stopwatch();
            task.Run();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            watch.Start();
            for (var i = 0; i < repetitionCount; i++)
                task.Run();
            watch.Stop();

            return watch.Elapsed.TotalMilliseconds / repetitionCount ;
        }
	}

    public class StringBuilderCreation : ITask
    {
        private int strLength;

        public StringBuilderCreation(int len)
        {
            strLength = len;
        }
        
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public void Run()
        {
            var str = new StringBuilder();
            for (var i = 0; i < strLength; i++)
                str.Append('0');
            str.ToString();
        } 
    }

    public class StringConstructorCreation : ITask
    {
        private int strLength;

        public StringConstructorCreation(int len)
        {
            strLength = len;
        }
        
        public void Run()
        {
            new string('0',strLength);
        }
    }

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        private const int RepetitionsCount = 50000;
        private const int StrLength = 10000;
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var benchmark = new Benchmark();
            var builderTime = benchmark.MeasureDurationInMs(new StringBuilderCreation(StrLength), RepetitionsCount);
            var structTime = benchmark.MeasureDurationInMs(new StringConstructorCreation(StrLength), RepetitionsCount);
            Assert.Less(structTime,builderTime);
        }
    }
}