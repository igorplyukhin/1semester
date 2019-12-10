using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace StructBenchmarking
{
    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            var chart = new ChartDataInfo(benchmark, repetitionsCount, f1, g1);

            return new ChartData
            {
                Title = "Create array",
                ClassPoints = chart.classesTimes,
                StructPoints = chart.structuresTimes,
            };
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            var chart = new ChartDataInfo(benchmark, repetitionsCount, g, f);

            return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = chart.classesTimes,
                StructPoints = chart.structuresTimes,
            };
        }

        public static MethodCallWithStructArgumentTask f(int size)
        {
            return new MethodCallWithStructArgumentTask(size);
        }

        public static MethodCallWithClassArgumentTask g(int size)
        {
            return new MethodCallWithClassArgumentTask(size);
        }

        public static StructArrayCreationTask f1(int size)
        {
            return new StructArrayCreationTask(size);
        }

        public static ClassArrayCreationTask g1(int size)
        {
            return new ClassArrayCreationTask(size);
        }
    }

    class ChartDataInfo
    {
        public List<ExperimentResult> classesTimes = new List<ExperimentResult>();
        public List<ExperimentResult> structuresTimes = new List<ExperimentResult>();

        public ChartDataInfo(IBenchmark benchmark, int repetitionsCount, Func<int, ITask> m1, Func<int, ITask> m2)
        {
            foreach (var size in Constants.FieldCounts)
            {
                var t = benchmark.MeasureDurationInMs(m1(size), repetitionsCount);
                structuresTimes.Add(new ExperimentResult(size, t));
                t = benchmark.MeasureDurationInMs(m2(size), repetitionsCount);
                classesTimes.Add(new ExperimentResult(size, t));
            }
        }
    }
}