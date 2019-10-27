using System;

namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            var horseStepLength = (Math.Max(width, height) - 2) / (Math.Min (width, height) - 2);
            if (width > height)
                while (robot.Finished == false)
                    MakeHorseStep(robot, horseStepLength, Direction.Right, Direction.Down);
            else
                while (robot.Finished == false)
                    MakeHorseStep(robot, horseStepLength, Direction.Down, Direction.Right);
        }

        public static void MakeHorseStep(Robot robot, int horseStepLength, Direction firstDirection, Direction secondDirection)
        {
            for (var i = 0; i < horseStepLength; i++)
                robot.MoveTo(firstDirection);
            if (!robot.Finished)
                robot.MoveTo(secondDirection);
        }
    }
}

