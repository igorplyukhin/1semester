using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NUnit.Framework;

namespace Manipulation
{
    public static class VisualizerTask
    {
        public static double X = 220;
        public static double Y = -100;
        public static double Alpha = 0.05;
        public static double Wrist = 2 * Math.PI / 3;
        public static double Elbow = 3 * Math.PI / 4;
        public static double Shoulder = Math.PI / 2;
        public static double RotationAngle = Math.PI / 30;
        public static int RectSize = 10;


        public static Brush UnreachableAreaBrush = new SolidBrush(Color.FromArgb(255, 255, 230, 230));
        public static Brush ReachableAreaBrush = new SolidBrush(Color.FromArgb(255, 230, 255, 230));
        public static Pen ManipulatorPen = new Pen(Color.Black, 3);
        public static Brush JointBrush = Brushes.Gray;

        public static void KeyDown(Form form, KeyEventArgs key)
        {
            var isChanged = true;
            switch (key.KeyCode)
            {
                case Keys.Q:
                    Shoulder += RotationAngle;
                    break;
                case Keys.A:
                    Shoulder -= RotationAngle;
                    break;
                case Keys.W:
                    Elbow += RotationAngle;
                    break;
                case Keys.S:
                    Elbow -= RotationAngle;
                    break;
                default:
                    isChanged = false;
                    break;
            }

            if (isChanged)
            {
                Wrist = -Alpha - Shoulder - Elbow;
                form.Invalidate();
            }
        }

        public static void MouseMove(Form form, MouseEventArgs e)
        {
            var newPoint = ConvertWindowToMath(new PointF(e.X, e.Y), GetShoulderPos(form));
            X = newPoint.X;
            Y = newPoint.Y;
            UpdateManipulator();
            form.Invalidate();
        }

        public static void MouseWheel(Form form, MouseEventArgs e)
        {
            Alpha += e.Delta;
            UpdateManipulator();
            form.Invalidate();
        }

        public static void UpdateManipulator()
        {
            var newAngles = ManipulatorTask.MoveManipulatorTo(X, Y, Alpha);
            if (!newAngles.Contains(double.NaN))
            {
                Shoulder = newAngles[0];
                Elbow = newAngles[1];
                Wrist = newAngles[2];
            }
        }

        public static void DrawManipulator(Graphics graphics, PointF shoulderPos)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);
            var windowJoints = joints.Select(x => ConvertMathToWindow(x, shoulderPos)).ToList();
            windowJoints.Insert(0, shoulderPos);
            graphics.DrawString(
                $"X={X:0}, Y={Y:0}, Alpha={Alpha:0.00}",
                new Font(SystemFonts.DefaultFont.FontFamily, 12),
                Brushes.DarkRed,
                10,
                10);
            DrawReachableZone(graphics, ReachableAreaBrush, UnreachableAreaBrush, shoulderPos, joints);
            for (var i = 0; i < windowJoints.Count - 1; i++)
            {
                var currentPoint = windowJoints[i];
                var nextPoint = windowJoints[i + 1];
                graphics.DrawLine(ManipulatorPen, currentPoint, nextPoint);
                graphics.FillEllipse(JointBrush, currentPoint.X + RectSize, currentPoint.Y + RectSize,
                    -2 * RectSize, -2 * RectSize);
            }
        }

        private static void DrawReachableZone(
            Graphics graphics,
            Brush reachableBrush,
            Brush unreachableBrush,
            PointF shoulderPos,
            PointF[] joints)
        {
            var rmin = Math.Abs(Manipulator.UpperArm - Manipulator.Forearm);
            var rmax = Manipulator.UpperArm + Manipulator.Forearm;
            var mathCenter = new PointF(joints[2].X - joints[1].X, joints[2].Y - joints[1].Y);
            var windowCenter = ConvertMathToWindow(mathCenter, shoulderPos);
            graphics.FillEllipse(reachableBrush, windowCenter.X - rmax, windowCenter.Y - rmax, 2 * rmax, 2 * rmax);
            graphics.FillEllipse(unreachableBrush, windowCenter.X - rmin, windowCenter.Y - rmin, 2 * rmin, 2 * rmin);
        }

        public static PointF GetShoulderPos(Form form)
        {
            return new PointF(form.ClientSize.Width / 2f, form.ClientSize.Height / 2f);
        }

        public static PointF ConvertMathToWindow(PointF mathPoint, PointF shoulderPos)
        {
            return new PointF(mathPoint.X + shoulderPos.X, shoulderPos.Y - mathPoint.Y);
        }

        public static PointF ConvertWindowToMath(PointF windowPoint, PointF shoulderPos)
        {
            return new PointF(windowPoint.X - shoulderPos.X, shoulderPos.Y - windowPoint.Y);
        }
    }
}