using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var birthCounts = new double[31];
            foreach (var e in names)
            {
                if (e.Name == name && e.BirthDate.Day != 1)
                    birthCounts[e.BirthDate.Day - 1]++;
            }

            var months = new string[31];
            for (var i = 0; i < months.Length; i++)
            {
                months[i] = (i + 1).ToString();
            }

            return new HistogramData(
                string.Format("Рождаемость людей с именем '{0}'", name),
                months,
                birthCounts);
        }
    }
}