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
                var left = Math.Max(r1.Left, r2.Left);
                var right = Math.Min(r1.Right, r2.Right);
                var top = Math.Max(r1.Top, r2.Top);
                var bottom = Math.Min(r1.Bottom, r2.Bottom);
                var xIntersection = Math.Max(right - left, 0);
                var yIntersection = Math.Max(bottom - top, 0);
                return xIntersection * yIntersection;
            }
            return 0;
        }

        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            var firstRectSquare = r1.Width * r1.Height;
            var secondRectSquare = r2.Width * r2.Height;
            if (IntersectionSquare(r1, r2) != 0)
            {
                if (secondRectSquare == IntersectionSquare(r1, r2) && firstRectSquare == IntersectionSquare(r1, r2))
                    return 1;
                if (IntersectionSquare(r1, r2) == firstRectSquare)
                    return 0;
                if (IntersectionSquare(r1, r2) == secondRectSquare)
                    return 1;
            }
            if (IsZeroSqaureCase(r1, firstRectSquare, r2, secondRectSquare))
                return 0;
            if (IsZeroSqaureCase(r2, secondRectSquare, r1, firstRectSquare))
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