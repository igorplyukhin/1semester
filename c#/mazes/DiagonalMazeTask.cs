using System;

namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            if (width > height)
            {
                SolveHorizontalMaze(robot, width, height);
            }
            else
            {
                SolveVerticalMaze(robot, width, height);
            }
        }

        static void Move(Robot robot, int aspectRatio, Direction firstDirection, Direction secondDirection)
        {
            if (!robot.Finished)
                for (var i = 0; i < aspectRatio; i++)
                    robot.MoveTo(firstDirection);
            if (!robot.Finished)
                robot.MoveTo(secondDirection);
        }

        static void SolveHorizontalMaze(Robot robot, int width, int height)
        {
            var aspectRatio = (width - 2) / (height - 2);
            while (robot.Finished == false)
                Move(robot, aspectRatio, Direction.Right, Direction.Down);
        }

        static void SolveVerticalMaze(Robot robot, int width, int height)
        {
            var aspectRatio = (height - 2) / (width - 2);
            while (robot.Finished == false)
                Move(robot, aspectRatio, Direction.Down, Direction.Right);
        }

    }
}

