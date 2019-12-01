using System;
using System.Drawing;
using System.Windows.Forms;

namespace Digger
{
    public class Terrain : ICreature
    {
        public string GetImageFileName() => "Terrain.png";

        public int GetDrawingPriority() => 1;

        public CreatureCommand Act(int x, int y) => new CreatureCommand();

        public bool DeadInConflict(ICreature conflictedObject) => true;
    }

    class Player : ICreature
    {
        public string GetImageFileName() => "Digger.png";

        public int GetDrawingPriority() => 0;

        public bool DeadInConflict(ICreature conflictedObject) =>
            conflictedObject is Sack || conflictedObject is Monster;

        public CreatureCommand Act(int x, int y)
        {
            var xDelta = 0;
            var yDelta = 0;
            switch (Game.KeyPressed)
            {
                case Keys.Left:
                    xDelta = -1;
                    break;
                case Keys.Right:
                    xDelta = 1;
                    break;
                case Keys.Up:
                    yDelta = -1;
                    break;
                case Keys.Down:
                    yDelta = 1;
                    break;
            }
            var xNext = x + xDelta;
            var yNext = y + yDelta;
            if (Checkers.IsInBounds(xNext, yNext, Game.MapWidth, Game.MapHeight)
                && Checkers.IsCorrectPlayerMove(xNext, yNext))
            {
                if (Game.Map[xNext, yNext] is Gold)
                    Game.Scores += 10;
                return new CreatureCommand {DeltaX = xDelta, DeltaY = yDelta};
            }
            return new CreatureCommand();
        }
    }

    class Sack : ICreature
    {
        public string GetImageFileName() => "Sack.png";

        public int GetDrawingPriority() => 2;

        public bool DeadInConflict(ICreature conflictedObject) => false;

        private int passedCellsCount;
        private CreatureCommand fallOneCell = new CreatureCommand {DeltaY = 1};
        private CreatureCommand transformToGold = new CreatureCommand {TransformTo = new Gold()};

        public CreatureCommand Act(int x, int y)
        {
            var isFalling = true;
            if (Checkers.IsCorrectSackMove(x, y + 1, passedCellsCount))
                passedCellsCount++;
            else
                isFalling = false;

            if (isFalling)
                return fallOneCell;
            if (passedCellsCount > 1)
                return transformToGold;
            passedCellsCount = 0;
            return new CreatureCommand();
        }
    }

    class Gold : ICreature
    {
        public string GetImageFileName() => "Gold.png";

        public int GetDrawingPriority() => 3;

        public CreatureCommand Act(int x, int y) => new CreatureCommand();

        public bool DeadInConflict(ICreature conflictedObject) => true;
    }

    class Monster : ICreature
    {
        public string GetImageFileName() => "Monster.png";

        public int GetDrawingPriority() => 0;

        public bool DeadInConflict(ICreature conflictedObject) =>
            conflictedObject is Monster || conflictedObject is Sack;

        public CreatureCommand Act(int x, int y)
        {
            var playerPos = new Point();
            if (TryGetPlayerPos(out playerPos))
            {
                var xDelta = Math.Sign(playerPos.X - x);
                var yDelta = xDelta == 0 ? Math.Sign(playerPos.Y - y) : 0;
                var xNext = x + xDelta;
                var yNext = y + yDelta;
                if (Checkers.IsInBounds(xNext, yNext, Game.MapWidth, Game.MapHeight)
                    && Checkers.IsCorrectMonsterMove(xNext, yNext))
                    return new CreatureCommand {DeltaX = xDelta, DeltaY = yDelta};

                yDelta = Math.Sign(playerPos.Y - y);
                xDelta = yDelta == 0 ? Math.Sign(playerPos.X - x) : 0;
                xNext = x + xDelta;
                yNext = y + yDelta;
                if (Checkers.IsInBounds(xNext, yNext, Game.MapWidth, Game.MapHeight)
                    && Checkers.IsCorrectMonsterMove(xNext, yNext))
                    return new CreatureCommand {DeltaX = xDelta, DeltaY = yDelta};
            }

            return new CreatureCommand();
        }

        private bool TryGetPlayerPos(out Point p)
        {
            var xBound = Game.MapWidth;
            var yBound = Game.MapHeight;
            for (var i = 0; i < xBound; i++)
            for (var j = 0; j < yBound; j++)
            {
                if (Game.Map[i, j] is Player)
                {
                    p = new Point(i, j);
                    return true;
                }
            }

            p = new Point();
            return false;
        }
    }

    class Checkers
    {
        public static bool IsInBounds(int x, int y, int xBound, int yBound) =>
            x < xBound && x >= 0 && y < yBound && y >= 0 && !(Game.Map[x, y] is Sack);

        public static bool IsCorrectMonsterMove(int x, int y) =>
            !(Game.Map[x, y] is Terrain || Game.Map[x, y] is Sack || Game.Map[x, y] is Monster);

        public static bool IsCorrectPlayerMove(int x, int y) => !(Game.Map[x, y] is Sack);

        public static bool IsCorrectSackMove(int x, int y, int pasedCellsCount)
            => y < Game.MapHeight
               && (Game.Map[x, y] == null || (Game.Map[x, y] is Player || Game.Map[x, y] is Monster)
                   && pasedCellsCount > 0);
    }
}