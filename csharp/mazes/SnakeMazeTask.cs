namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            var raw = 1;
            while (raw != height)
            {
                MakeOneSnake(robot, width);
                if (!robot.Finished)
                    Move(robot, 2, Direction.Down);
                raw += 4;
            }
        }

        static void Move(Robot robot, int steps, Direction direction)
        {
            for (var i = 0; i < steps; i++)
            {
                robot.MoveTo(direction);
            }
        }

        static void MakeOneSnake(Robot robot, int width)
        {
            Move(robot, width - 3, Direction.Right);
            Move(robot, 2, Direction.Down);
            Move(robot, width - 3, Direction.Left);
        }
    }
}