using System;

namespace Names
{
    internal static class HeatmapTask
    {
        const int DaysCount = 30;
        const int MonthsCount = 12;

        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            return new HeatmapData(
                "Пример карты интенсивностей", 
                GetBirths(DaysCount, MonthsCount, names),
                GetHistogramLabels(DaysCount, 2),
                GetHistogramLabels(MonthsCount, 1));
        }

        private static string[] GetHistogramLabels(int count, int step)
        {
            var lables = new string[count];
            for (var i = 0; i < count; i++)
                lables[i] = (i + step).ToString();
            return lables;
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
