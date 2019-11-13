using System;
using System.Drawing;
using NUnit.Framework;
using System.Linq;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        public static double[] MoveManipulatorTo(double x, double y, double alpha)
        {
            var xWristPosition = x - Manipulator.Palm * Math.Cos(alpha);
            var yWristPosition = y + Manipulator.Palm * Math.Sin(alpha);
            var shoulderWristLength =
                Math.Sqrt(xWristPosition * xWristPosition + yWristPosition * yWristPosition);
            var elbow = TriangleTask.GetABAngle(Manipulator.UpperArm, Manipulator.Forearm, shoulderWristLength);
            var shoulder = TriangleTask.GetABAngle(Manipulator.UpperArm, shoulderWristLength, Manipulator.Forearm)
                           + Math.Atan2(yWristPosition, xWristPosition);
            var wrist = -alpha - elbow - shoulder;
            return double.IsNaN(wrist) || double.IsNaN(elbow) || double.IsNaN(shoulder)
                ? new[] {double.NaN, double.NaN, double.NaN}
                : new[] {shoulder, elbow, wrist};
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [Test]
        public void TestMoveManipulatorTo()
        {
            var random = new Random();
            for (var i = 0; i < 1000; i++)
            {
                var x = random.Next(100) + random.NextDouble();
                var y = random.Next(100) + random.NextDouble();
                var alpha = random.Next(100) + random.NextDouble();
                var actualAngles = ManipulatorTask.MoveManipulatorTo(x, y, alpha);
                if (!actualAngles.Contains(double.NaN))
                {
                    var actualCoordinates =
                        AnglesToCoordinatesTask.GetJointPositions(actualAngles[0], actualAngles[1], actualAngles[2]);
                    Assert.AreEqual(x, actualCoordinates[2].X, 1e-3, "X");
                    Assert.AreEqual(y, actualCoordinates[2].Y, 1e-3, "Y");
                }
                else
                    Assert.Pass();
            }
        }
    }
}