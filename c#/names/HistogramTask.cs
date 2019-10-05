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
                GetDays(31),
                GetBirthCounts(31, names, name));
        }

        private static string[] GetDays(int daysCount)
        {
            var months = new string[daysCount];
            for (var i = 0; i < months.Length; i++)
                months[i] = (i + 1).ToString();
            return months;
        }

        private static double[] GetBirthCounts(int daysCount, NameData[] names, string name)
        {
            var birthCounts = new double[daysCount];
            foreach (var e in names)
                if (e.Name == name && e.BirthDate.Day != 1)
                    birthCounts[e.BirthDate.Day - 1]++;
            return birthCounts;
        }
    }
}