using System;
using NUnit.Framework;

namespace Manipulation
{
    public class TriangleTask
    {
        public static double GetABAngle(double a, double b, double c)
        {
            return a > 0 && b > 0 && c >= 0 && a + b > c && a + c >= b && b + c >= a
                ? Math.Acos((a * a + b * b - c * c) / (2 * b * a))
                : double.NaN;
        }
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(3, 4, 5, Math.PI / 2)]
        [TestCase(0, 1, 1, double.NaN)]
        [TestCase(1, 0, 1, double.NaN)]
        [TestCase(1, 1, 0, 0)]
        [TestCase(10, 1, 1, double.NaN)]
        [TestCase(1, 10, 1, double.NaN)]
        [TestCase(1, 1, 10, double.NaN)]
        [TestCase(-1, -1, -1, double.NaN)]
        [TestCase(1, 1, -1, double.NaN)]
        [TestCase(0.1, 0.1, 0.1, Math.PI / 3)]
        [TestCase(1.5, 2.5, 3.5, Math.PI * 2 / 3)]
        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            Assert.AreEqual(expectedAngle, TriangleTask.GetABAngle(a, b, c), 1e-6);
        }
    }
}