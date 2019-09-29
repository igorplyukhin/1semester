namespace Mazes
{
    public static class EmptyMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
			MoveUntilWall(robot, width, Direction.Right);
			MoveUntilWall(robot, height, Direction.Down);
        }

		static void MoveUntilWall (Robot robot, int wall, Direction direction)
		{
			for (var i = 0; i< wall - 3; i++)
			{
				robot.MoveTo(direction);
			}
		}
    }
}