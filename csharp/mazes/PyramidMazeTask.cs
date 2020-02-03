namespace Mazes
{
    public static class PyramidMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            var i = 3;
            while (true)
            {
                Move(robot, width - i, Direction.Right);
                Move(robot, 2, Direction.Up);
                i += 2;
                Move(robot, width - i, Direction.Left);
                if (!robot.Finished)
                    Move(robot, 2, Direction.Up);
                else
                    break;
                i += 2;
            }
        }

        static void Move(Robot robot, int steps, Direction direction)
        {
            for (var i = 0; i < steps; i++)
            {
                robot.MoveTo(direction);
            }
        }
    }
}