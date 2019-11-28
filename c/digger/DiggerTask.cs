using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using NUnit.Framework.Constraints;

namespace Digger
{
    public class Terrain : ICreature
    {
        public string GetImageFileName() => "Terrain.png";
        public int GetDrawingPriority() => 3;
        public CreatureCommand Act(int x, int y) => new CreatureCommand();
        public bool DeadInConflict(ICreature conflictedObject) => conflictedObject is Player;
    }

    class Player : ICreature
    {
        public string GetImageFileName() => "Digger.png";
        public int GetDrawingPriority() => 2;
        public bool DeadInConflict(ICreature conflictedObject) => 
            conflictedObject is Sack || conflictedObject is Monster;

        private bool isCorrectMove(int x, int y, int xBound, int yBound) =>
            x < xBound && x >= 0 && y < yBound && y >= 0 && !(Game.Map[x, y] is Sack);

        public CreatureCommand Act(int x, int y)
        {
            var xDelta = 0;
            var yDelta = 0;
            switch (Game.KeyPressed)
            {
                case Keys.Left:
                    xDelta = isCorrectMove(x - 1, y, Game.MapWidth, Game.MapHeight) ? -1 : 0;
                    break;
                case Keys.Right:
                    xDelta = isCorrectMove(x + 1, y, Game.MapWidth, Game.MapHeight) ? 1 : 0;
                    break;
                case Keys.Up:
                    yDelta = isCorrectMove(x, y - 1, Game.MapWidth, Game.MapHeight) ? -1 : 0;
                    break;
                case Keys.Down:
                    yDelta = isCorrectMove(x, y + 1, Game.MapWidth, Game.MapHeight) ? 1 : 0;
                    break;
            }

            return new CreatureCommand {DeltaX = xDelta, DeltaY = yDelta};
        }
    }

    class Sack : ICreature
    {
        private int pasedCellsCount;
        public string GetImageFileName() => "Sack.png";
        public int GetDrawingPriority() => 1;
        public bool DeadInConflict(ICreature conflictedObject) => false;

        public CreatureCommand Act(int x, int y)
        {
            var yDelta = 0;
            var isFalling = true;
            if (y < Game.MapHeight - 1 && Game.Map[x, y + 1] == null)
            {
                yDelta = 1;
                pasedCellsCount++;
            }
            else if (y < Game.MapHeight - 1 && pasedCellsCount > 0 
                                            && (Game.Map[x, y + 1] is Player || Game.Map[x, y + 1] is Monster))
            {
                yDelta = 1;
                pasedCellsCount++;
            }
            else
                isFalling = false;

            if (isFalling)
                return new CreatureCommand {DeltaY = yDelta};
            if (pasedCellsCount > 1)
                return new CreatureCommand {TransformTo = new Gold()};
            pasedCellsCount = 0;
            return new CreatureCommand();
        }
    }

    class Gold : ICreature
    {
        public string GetImageFileName() => "Gold.png";
        public int GetDrawingPriority() => 3;
        public CreatureCommand Act(int x, int y) => new CreatureCommand();
        public bool DeadInConflict(ICreature conflictedObject)
        {
            Game.Scores += 10;
            return conflictedObject is Player || conflictedObject is Monster;
        }
    }

    class Monster : ICreature
    {
        public string GetImageFileName() => "Monster.png";
        public int GetDrawingPriority() => 0;
        public bool DeadInConflict(ICreature conflictedObject) => 
            conflictedObject is Monster || conflictedObject is Sack;
        private bool isCorrectMove(int x, int y) => 
            !(Game.Map[x, y] is Terrain || Game.Map[x, y] is Sack || Game.Map[x, y] is Monster);

        public CreatureCommand Act(int x, int y)
        {
            var xDelta = 0;
            var yDelta = 0;
            var playerPos = GetPlayerPosition();
            if (playerPos.X == -1)
                return new CreatureCommand();
            if (playerPos.X < x)
                xDelta = -1;
            else if (playerPos.X > x)
                xDelta = 1;
            else
            {
                if (playerPos.Y < y)
                    yDelta = -1;
                else
                    yDelta = 1;
            }

            return isCorrectMove(x + xDelta, y + yDelta)
                ? new CreatureCommand {DeltaX = xDelta, DeltaY = yDelta}
                : new CreatureCommand();
        }

        private Point GetPlayerPosition()
        {
            var xBound = Game.MapWidth;
            var yBound = Game.MapHeight;
            for (var i = 0; i < xBound; i++)
            for (var j = 0; j < yBound; j++)
            {
                if (Game.Map[i, j] is Player)
                    return new Point(i, j);
            }

            return new Point(-1, -1);
        }
    }
}