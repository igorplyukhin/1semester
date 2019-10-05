using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Names
{
    internal static class CreativityTask
    {
        public static HistogramData GetMostOftenNameHistogram(NameData[] names)
        {
            var minYear = int.MaxValue;
            var maxYear = int.MinValue;
            var mostOftenName = "";
            var birthsMax = 0;
            var namesCount = new Dictionary<string, int>();
            foreach (var person in names)
            {
                minYear = Math.Min(minYear, person.BirthDate.Year);
                maxYear = Math.Max(maxYear, person.BirthDate.Year);
                if (!namesCount.ContainsKey(person.Name))
                    namesCount.Add(person.Name, 1);
                else
                    namesCount[person.Name]++;
            }

            foreach (var name in namesCount)
            {
                if (name.Value > birthsMax)
                {
                    birthsMax = name.Value;
                    mostOftenName = name.Key;
                }
            }

            return new HistogramData(
                string.Format("Визуализация рождаемости имени {0}",
                mostOftenName),
                GetYears(minYear, maxYear),
                GetBirths(names, mostOftenName, birthsMax, minYear)
               );
        }

        private static string[] GetYears(int minYear, int maxYear)
        {
            var years = new string[maxYear - minYear + 1];
            for (var y = 0; y < years.Length; y++)
                years[y] = (y + minYear).ToString();
            return years;
        }

        private static double[] GetBirths(NameData[] names, string mostOftenName, int birthsMax, int minYear)
        {
            var birthsCount = new double[birthsMax];
            foreach (var person in names)
            {
                if (person.Name == mostOftenName)
                    birthsCount[person.BirthDate.Year - minYear]++;
            }
            return birthsCount;
        }
    }
}
