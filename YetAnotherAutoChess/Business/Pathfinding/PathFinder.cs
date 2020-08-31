using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetAnotherAutoChess.Business.GameAssets.BoardAssets;
using YetAnotherAutoChess.Enums;

namespace YetAnotherAutoChess.Business.Pathfinding
{
    public class PathFinder : IPathfindable
    {
        private IPathfindable _algorithm;
        public IPathfindable Algorithm
        {
            get => _algorithm;
            set
            { 
                _algorithm = value;
                _algorithm.SetGraph(_graph);
            }
        }

        private FigureGraph _graph;

        public PathFinder(IPathfindable algorithm)
        {
            SetupSingleton();
            _algorithm = algorithm;
        }

        public void SetGraph(FigureGraph graph)
        {
            _graph = graph;
            _algorithm.SetGraph(graph);
        }

        public Point FindNextStep(Figure source, bool isKnight = false)
        {
            return _algorithm.FindNextStep(source, isKnight);
        }

        public bool IsEnemyInRange(Figure source, Figure target)
        {
            return _algorithm.IsEnemyInRange(source, target);
        }

        public Figure EnemyInsideRange(Figure source, int range, int targetRow = -1, int targetColumn = -1,
            TargetingSystem targetingSystem = Enums.TargetingSystem.ClosestEnemy)
        {
            return _algorithm.EnemyInsideRange(source, range, targetRow, targetColumn, targetingSystem);
        }

        public Point KnightJumpOnStart(Figure source)
        {
            return _algorithm.KnightJumpOnStart(source);
        }

        #region Singleton
        public static PathFinder Instance;


        /// <summary>
        /// Creates a single instance of this object
        /// </summary>
        private void SetupSingleton()
        {
            if (PathFinder.Instance != null && PathFinder.Instance != this)
            {
                return;
            }

            PathFinder.Instance = this;
        }

        #endregion
    }
}
