using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            return new HistogramData(
                string.Format("Рождаемость людей с именем '{0}'", name),
                GetMonths(31),
                GetBirthCounts(31, names, name));
        }

        private static string[] GetMonths(int arraySize)
        {
            var months = new string[arraySize];
            for (var i = 0; i < months.Length; i++)
                months[i] = (i + 1).ToString();
            return months;
        }

        private static double[] GetBirthCounts(int arraySize, NameData[] names, string name)
        {
            var birthCounts = new double[arraySize];
            foreach (var e in names)
                if (e.Name == name && e.BirthDate.Day != 1)
                    birthCounts[e.BirthDate.Day - 1]++;
            return birthCounts;
        }
    }
}