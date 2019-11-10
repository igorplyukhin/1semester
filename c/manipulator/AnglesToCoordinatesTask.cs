using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var currentAngle = shoulder;
            var elbowPos = CalcCoordinates(currentAngle, Manipulator.UpperArm, 0, 0);
            currentAngle += elbow + Math.PI;
            var wristPos = CalcCoordinates(currentAngle, Manipulator.Forearm, elbowPos.X, elbowPos.Y);
            currentAngle += wrist + Math.PI;
            var palmEndPos = CalcCoordinates(currentAngle, Manipulator.Palm, wristPos.X, wristPos.Y);
            return new []
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }

        private static PointF CalcCoordinates(double angle, double length, double x0, double y0) =>
            new PointF((float) (length * Math.Cos(angle) + x0), (float) (length * Math.Sin(angle) + y0));
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(-Math.PI / 2, Math.PI / 2, -Math.PI / 2, -120, -210)]
        [TestCase(0, Math.PI, Math.PI, 330, 0)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, 180, 150)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            Assert.AreEqual(Manipulator.UpperArm, CalcJointsLength(joints[0]));
            Assert.AreEqual(Manipulator.Forearm, CalcJointsLength(GetVectorCoord(joints[0], joints[1])));
            Assert.AreEqual(Manipulator.Palm, CalcJointsLength(GetVectorCoord(joints[1], joints[2])));
        }

        private double CalcJointsLength(PointF joint) => Math.Sqrt(joint.X * joint.X + joint.Y * joint.Y);
        private PointF GetVectorCoord(PointF a, PointF b) => new PointF(b.X - a.X, b.Y - a.Y);
    }
}