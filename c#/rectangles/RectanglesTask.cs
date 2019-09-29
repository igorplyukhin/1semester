using System;

namespace Rectangles
{
    public static class RectanglesTask
    {
        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            var tooLeft = r1.Left > r2.Left + r2.Width;
            var tooRight = r2.Left > r1.Left + r1.Width;
            var tooHigh = r1.Top > r2.Top + r2.Height;
            var tooLow = r2.Top > r1.Top + r1.Height;
            return !(tooLeft || tooRight || tooHigh || tooLow);
        }

        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            if (AreIntersected(r1, r2))
            {
                var xIntersection = SearchIntersection(r1.Left, r1.Right, r2.Left, r2.Right);
                var yIntersection = SearchIntersection(r1.Top, r1.Bottom, r2.Top, r2.Bottom);
                return xIntersection * yIntersection;
            }
            return 0;
        }
        static int SearchIntersection(int aLeft, int aRight, int bLeft, int bRight)
        {
            var left = Math.Max(aLeft, bLeft);
            var right = Math.Min(aRight, bRight);
            return Math.Max(right - left, 0);
        }

        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            var s1 = r1.Width * r1.Height;
            var s2 = r2.Width * r2.Height;
            if (IntersectionSquare(r1, r2) != 0)
            {
                if (s2 == IntersectionSquare(r1, r2) && s1 == IntersectionSquare(r1, r2))
                    return 1;
                if (IntersectionSquare(r1, r2) == s1)
                    return 0;
                if (IntersectionSquare(r1, r2) == s2)
                    return 1;
            }
            if (IsZeroSqaureCase(r1, s1, r2, s2))
                return 0;
            if (IsZeroSqaureCase(r2, s2, r1, s1))
                return 1;
            return -1;
        }

        static bool IsZeroSqaureCase(Rectangle r1, int s1, Rectangle r2, int s2)
        {
            return s1 == 0 && r1.Left >= r2.Left && r1.Top >= r2.Top
            && r1.Right <= r2.Right && r1.Bottom <= r2.Bottom;
        }
    }
}