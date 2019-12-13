using System;
using System.Collections.Generic;

namespace StructBenchmarking
{
    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            var chart = new ChartDataInfo(benchmark, repetitionsCount, Functions.CreateStructArray,
                Functions.CreateClassArray);

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
            var chart = new ChartDataInfo(benchmark, repetitionsCount, Functions.CreateStruct,
                Functions.CreateClass);

            return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = chart.ClassesTimes,
                StructPoints = chart.StructuresTimes,
            };
        }
    }

    static class Functions
    {
        public static MethodCallWithStructArgumentTask CreateStruct(int size) =>
            new MethodCallWithStructArgumentTask(size);

        public static MethodCallWithClassArgumentTask CreateClass(int size) =>
            new MethodCallWithClassArgumentTask(size);

        public static StructArrayCreationTask CreateStructArray(int size) =>
            new StructArrayCreationTask(size);

        public static ClassArrayCreationTask CreateClassArray(int size) =>
            new ClassArrayCreationTask(size);
    }

    class ChartDataInfo
    {
        public List<ExperimentResult> ClassesTimes = new List<ExperimentResult>();
        public List<ExperimentResult> StructuresTimes = new List<ExperimentResult>();

        public ChartDataInfo(IBenchmark benchmark, int repetitionsCount, Func<int, ITask> m1, Func<int, ITask> m2)
        {
            foreach (var size in Constants.FieldCounts)
            {
                var time = benchmark.MeasureDurationInMs(m1(size), repetitionsCount);
                StructuresTimes.Add(new ExperimentResult(size, time));
                time = benchmark.MeasureDurationInMs(m2(size), repetitionsCount);
                ClassesTimes.Add(new ExperimentResult(size, time));
            }
        }
    }
}