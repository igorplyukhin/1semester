using System;
using System.Collections.Generic;

namespace StructBenchmarking
{
    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            var chart = new ChartDataCreation(benchmark, repetitionsCount,
                x => new StructArrayCreationTask(x), x => new ClassArrayCreationTask(x));

            return new ChartData
            {
                Title = "Create array",
                ClassPoints = chart.ClassesTimes,
                StructPoints = chart.StructuresTimes,
            };
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            var chart = new ChartDataCreation(benchmark, repetitionsCount,
                x => new MethodCallWithStructArgumentTask(x), 
                x => new MethodCallWithClassArgumentTask(x));

            return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = chart.ClassesTimes,
                StructPoints = chart.StructuresTimes,
            };
        }
    }

    class ChartDataCreation
    {
        private readonly List<ExperimentResult> classesTimes = new List<ExperimentResult>();
        private readonly List<ExperimentResult> structuresTimes = new List<ExperimentResult>();
        public List<ExperimentResult> ClassesTimes => classesTimes;
        public List<ExperimentResult> StructuresTimes => structuresTimes;

        public ChartDataCreation(
            IBenchmark benchmark, int repetitionsCount, Func<int, ITask> structTask, Func<int, ITask> classTask)
        {
            foreach (var size in Constants.FieldCounts)
            {
                var time = benchmark.MeasureDurationInMs(structTask(size), repetitionsCount);
                structuresTimes.Add(new ExperimentResult(size, time));
                time = benchmark.MeasureDurationInMs(classTask(size), repetitionsCount);
                classesTimes.Add(new ExperimentResult(size, time));
            }
        }
    }
}