using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var picWidth = original.GetLength(0);
            var picHeight = original.GetLength(1);
            var t = CalcThresholdValue(original, whitePixelsFraction);
            var filteredPic = new double[picWidth, picHeight];
            for (var i = 0; i < picWidth; i++)
            {
                for (var j = 0; j < picHeight; j++)
                {
                    filteredPic[i, j] = original[i, j] >= t ? 1.0 : 0.0;
                }
            }

            return filteredPic;
        }

        private static double CalcThresholdValue(double[,] original, double whitePixelsFraction)
        {
            var picWidth = original.GetLength(0);
            var picHeight = original.GetLength(1);
            var pixelsCount = picWidth * picHeight;
            var whitePixelsCount = (int) (pixelsCount * whitePixelsFraction);
            return whitePixelsCount > 0
                ? original.Cast<double>().OrderByDescending(x => x).ElementAt(whitePixelsCount - 1)
                : Double.MaxValue;
        }
    }
}