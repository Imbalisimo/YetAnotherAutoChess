using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.Pathfinding
{
    public interface IPathfindable
    {
        void SetGraph(GameAssets.BoardAssets.FigureGraph graph);
        bool IsEnemyInRange(Figure source, Figure target);
        Figure EnemyInsideRange(Figure source, int range, int targetRow, int targetColumn, Enums.TargetingSystem targetingSystem);
        Point FindNextStep(Figure source, bool isKnight);
        Point KnightJumpOnStart(Figure source);
    }
}
