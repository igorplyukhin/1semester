using System;

namespace DistanceTask
{
    public static class DistanceTask
    {
        // Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            var akLength = GetVectorLength(ax, ay, x, y);
            var bkLength = GetVectorLength(bx, by, x, y);
            var abLength = GetVectorLength(ax, ay, bx, by);

            var scalatMultAKAB = (x - ax) * (bx - ax) + (y - ay) * (by - ay);
            var scalatMultBKAB = (x - bx) * (ax - bx) + (y - by) * (ay - by);

            if (abLength == 0)
                return akLength;
            if (scalatMultAKAB >= 0 && scalatMultBKAB >= 0)
            {
                var p = (akLength + bkLength + abLength) / 2.0;
                var s = Math.Sqrt(Math.Abs(p * (p - akLength) * (p - bkLength) * (p - abLength)));
                return 2.0 * s / abLength;
            }
            if (scalatMultAKAB < 0 || scalatMultBKAB < 0)
            {
                return Math.Min(akLength, bkLength);
            }
            return 0.0;
        }

        static double GetVectorLength(double ax, double ay, double bx, double by)
        {
            return Math.Sqrt((bx - ax) * (bx - ax) + (by - ay) * (by - ay));
        }
    }
}