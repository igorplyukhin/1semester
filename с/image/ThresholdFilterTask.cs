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
            var pixelsCount = picWidth * picHeight;
            var whitePixelsCount = (int) (pixelsCount * whitePixelsFraction);
            var pixelsSortedByValue = original.Cast<double>().OrderByDescending(x => x).ToList();
            var t = whitePixelsCount > 0 ? pixelsSortedByValue.ElementAt(whitePixelsCount - 1) : Double.MaxValue;
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
    }
}