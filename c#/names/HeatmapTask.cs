using System;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var months = new string[12];
            for (int i = 0; i < 12; i++)
                months[i] = (i + 1).ToString();

            var days = new string[30];
            for (int i = 0; i < 30; i++)
                days[i] = (i + 2).ToString();

            var Births = new double[30, 12];
            foreach (var name in names)
                if (name.BirthDate.Day != 1)
                    Births[name.BirthDate.Day - 2, name.BirthDate.Month - 1]++;

            return new HeatmapData("Пример карты интенсивностей", Births, days, months);
        }
    }
}
