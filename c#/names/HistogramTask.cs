using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        const int DaysCount = 31;
        const int Step = 1;

        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            return new HistogramData(
                string.Format("Рождаемость людей с именем '{0}'", name),
                GetHistogramLabels(DaysCount, Step),
                GetBirthCounts(DaysCount, names, name));
        }

        private static string[] GetHistogramLabels(int count, int step)
        {
            var labels = new string[count];
            for (var i = 0; i < labels.Length; i++)
                labels[i] = (i + step).ToString();
            return labels;
        }

        private static double[] GetBirthCounts(int count, NameData[] names, string name)
        {
            var birthCounts = new double[count];
            foreach (var e in names)
                if (e.Name == name && e.BirthDate.Day != 1)
                    birthCounts[e.BirthDate.Day - 1]++;
            return birthCounts;
        }
    }
}