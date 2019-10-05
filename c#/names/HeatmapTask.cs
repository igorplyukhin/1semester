using System;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            return new HeatmapData(
                "Пример карты интенсивностей", 
                GetBirths(30, 12, names), 
                GetDays(30), 
                GetMonths(12));
        }

        private static string[] GetMonths(int monthsCount)
        {
            var months = new string[monthsCount];
            for (var i = 0; i < 12; i++)
                months[i] = (i + 1).ToString();
            return months;
        }

        private static string[] GetDays(int daysCount)
        {
            var days = new string[daysCount];
            for (var i = 0; i < 30; i++)
                days[i] = (i + 2).ToString();
            return days;
        }

        private static double[,] GetBirths(int daysCount, int monthsCount, NameData[] names)
        {
            var births = new double[daysCount, monthsCount];
            foreach (var name in names)
                if (name.BirthDate.Day != 1)
                    births[name.BirthDate.Day - 2, name.BirthDate.Month - 1]++;
            return births;
        }
    }
}
