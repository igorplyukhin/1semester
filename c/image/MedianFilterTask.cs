using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        static Tuple<int, int>[] neighbours =
        {
            Tuple.Create(-1, -1), Tuple.Create(-1, 0), Tuple.Create(-1, 1), 
            Tuple.Create(1, 1), Tuple.Create(1, 0), Tuple.Create(1, -1), 
            Tuple.Create(0, 1), Tuple.Create(0, -1), Tuple.Create(0, 0)
        };

        public static double[,] MedianFilter(double[,] original)
        {
            var picWidth = original.GetLength(0);
            var picHeight = original.GetLength(1);
            var filteredPIc = new double[picWidth, picHeight];
            for (var i = 0; i < picWidth; i++)
            {
                for (var j = 0; j < picHeight; j++)
                {
                    var closePixels = GetClosePixels(i, j, picWidth, picHeight, original);
                    filteredPIc[i, j] = CalcMedianValue(closePixels, i, j);
                }
            }

            return filteredPIc;
        }

        private static List<double> GetClosePixels(int i, int j,
            int picWidth, int picHeight, double[,] original)
        {
            var closePixels = new List<double>();
            foreach (var point in neighbours)
            {
                var iNeighbor = i + point.Item1;
                var jNeighbor = j + point.Item2;
                if (IsValidIndex(iNeighbor, jNeighbor, picWidth, picHeight))
                    closePixels.Add(original[iNeighbor, jNeighbor]);
            }

            return closePixels;
        }

        private static double CalcMedianValue(List<double> closePixels, int i, int j)
        {
            closePixels.Sort();
            var closePixelsLen = closePixels.Count;
            return closePixelsLen % 2 == 1
                ? closePixels[closePixelsLen / 2]
                : (closePixels[closePixelsLen / 2 - 1] + closePixels[closePixelsLen / 2]) / 2;
        }

        private static bool IsValidIndex(int i, int j, int width, int height)
        {
            return i >= 0 && i < width && j >= 0 && j < height;
        }
    }
}