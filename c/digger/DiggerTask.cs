using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Digger
{
    public class Terrain : ICreature
    {
        public string GetImageFileName() => "Terrain.png";
        public int GetDrawingPriority() => 1;
        public CreatureCommand Act(int x, int y) => new CreatureCommand();
        public bool DeadInConflict(ICreature conflictedObject) => conflictedObject.GetImageFileName() == "Digger.png";

    }

    class Player : ICreature
    {
        public string GetImageFileName() => "Digger.png";
        public int GetDrawingPriority() => 0;

        public CreatureCommand Act(int x, int y)
        {
            var xDelta = 0;
            var yDelta = 0;
            switch (Game.KeyPressed)
            {
                case Keys.Left:
                    xDelta = isCorrectMove(x - 1, Game.MapWidth) ? -1 : 0;
                    break;
                case Keys.Right:
                    xDelta = isCorrectMove(x + 1, Game.MapWidth) ? 1 : 0;
                    break;
                case Keys.Up:
                    yDelta = isCorrectMove(y - 1, Game.MapHeight) ? -1 : 0;
                    break;
                case Keys.Down:
                    yDelta = isCorrectMove(y + 1, Game.MapHeight) ? 1 : 0;
                    break;
            }

            return new CreatureCommand {DeltaX = xDelta, DeltaY = yDelta};
        }

        public bool DeadInConflict(ICreature conflictedObject) => false;
        private bool isCorrectMove(int coord, int bound) => coord < bound && coord >= 0;
    }

    class Sack : ICreature
    {
        private int pasedOneCell = 0;
        public string GetImageFileName() => "Sack.png";
        public int GetDrawingPriority() => 2;

        public CreatureCommand Act(int x, int y)
        {
            var yDelta = 0;
            if (y < Game.MapHeight - 1)
                if (Game.Map[x, y + 1] == null)
                {
                    yDelta++;
                    pasedOneCell++;
                }
                else
                    switch (Game.Map[x, y].ToString())
                    {
                        case "Digger.png":
                            Game.IsOver = true;
                            break;
                    }

            return pasedOneCell > 1
                ? new CreatureCommand {DeltaY = yDelta, TransformTo = new Gold()}
                : new CreatureCommand {DeltaY = yDelta};
        }

        public bool DeadInConflict(ICreature conflictedObject) => false;
    }

    class Gold : ICreature
    {
        public string GetImageFileName() => "Gold.png";

        public int GetDrawingPriority() => 3;

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject) => false;
    }
}