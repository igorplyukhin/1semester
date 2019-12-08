using System;
using System.Diagnostics;
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

            return (double)watch.ElapsedMilliseconds / repetitionCount ;
        }
	}

    public class StringBuilderCreation : ITask
    {
        public void Run()
        {
            var str = new StringBuilder();
            for (var i = 0; i < 10000; i++)
            {
                str.Append('0');
            }

            str.ToString();
        } 
    }

    public class StringConstructorCreation : ITask
    {
        public void Run()
        {
            new string('0',10000);
        }
    }

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var benchmark = new Benchmark();
            var builderTime = benchmark.MeasureDurationInMs(new StringBuilderCreation(), 50000);
            var structTime = benchmark.MeasureDurationInMs(new StringConstructorCreation(), 50000);
            Assert.Less(structTime,builderTime);
        }
    }
}