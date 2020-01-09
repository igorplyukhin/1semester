using System.Drawing;
using System;


namespace Fractals
{
    internal static class DragonFractalTask
    {
        static double sin45 = Math.Sin(ConvertToRad(45.0)),
            cos45 = Math.Cos(ConvertToRad(45.0)),
            sin135 = Math.Sin(ConvertToRad(135.0)),
            cos135 = Math.Cos(ConvertToRad(135.0));

        public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
        {
            var random = new Random(seed);
            var x = 1.0;
            var y = 0.0;
            for (var i = 0; i < iterationsCount; i++)
            {
                var nextNumber = random.Next(2);
                var x1 = GetNextPoint(x, y, nextNumber)[0];
                var y1 = GetNextPoint(x, y, nextNumber)[1];
                x = x1;
                y = y1;
                pixels.SetPixel(x, y);
            }
        }

        static double[] GetNextPoint(double x, double y, int nextNumber)
        {
            if (nextNumber == 1)
                return new[] {
                                (x * cos45 - y * sin45) / Math.Sqrt(2),
                                (x * sin45 + y * cos45) / Math.Sqrt(2)
                             };
            return new[] {
                            (x * cos135 - y * sin135) / Math.Sqrt(2) + 1,
                            (x * sin135 + y * cos135) / Math.Sqrt(2)
                         };
        }

        static double ConvertToRad(double gradus)
        {
            return Math.PI * gradus / 180;
        }

    }
}