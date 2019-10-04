using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var birthCounts = GetBirthCounts(names, name);
            var months = GetMonths();
            return new HistogramData(
                string.Format("Рождаемость людей с именем '{0}'", name),
                months,
                birthCounts);
        }

        private static string[] GetMonths()
        {
            var months = new string[31];
            for (var i = 0; i < months.Length; i++)
            {
                months[i] = (i + 1).ToString();
            }
            return months;
        }

        private static double[] GetBirthCounts(NameData[] names, string name)
        {
            var birthCounts = new double[31];
            foreach (var e in names)
            {
                if (e.Name == name && e.BirthDate.Day != 1)
                    birthCounts[e.BirthDate.Day - 1]++;
            }
            return birthCounts;
        }
    }
}