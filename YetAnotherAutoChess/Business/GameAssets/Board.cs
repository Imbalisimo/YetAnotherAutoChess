using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetAnotherAutoChess.Business.GameAssets.BoardAssets;

namespace YetAnotherAutoChess.Business.GameAssets
{
    public static class Board
    {
        private static int _rows = 8;
        private static int _columns = 8;
        private static BoardTile _boardTiles;

        public static string Owner { get; }

        public static FigureGraph Figures { get; set; }

        public static List<Figure> AllFigures;
        private static List<Figure> _activeAllyFigures;
        private static List<Figure> _activeEnemyFigures;

        public delegate void MatchLost(int damage);
        public static event MatchLost OnMatchEnd;

        private static PieceCounter _pieceCounter;

        public static bool SpawnFigure(Unit unit, Enums.Piece piece)
        {
            for (int i = 0; i < 8; ++i)
            {
                if (Figures[-1, i] == null)
                {
                    Figure figureOnBoard = FigureManager.CreateFigure(unit, piece);

                    if (CheckForUpgrades(figureOnBoard, true).Count != 0)
                        return true;

                    figureOnBoard.Untargetable = true;
                    figureOnBoard.Owner = Owner;
                    figureOnBoard.OnDeath += f =>
                    {
                        Figures[f.Position.Row, f.Position.Column] = null;
                        _activeAllyFigures.Remove(f);
                        if (_activeAllyFigures.Count == 0)
                            TakeDamage();
                    };
                    figureOnBoard.OnMove += MoveFigure;
                    figureOnBoard.OnSell += figure => SellFigure(figure);

                    AllFigures.Add(figureOnBoard);
                    SpawnChessFigure(AllFigures.Count - 1, -1, i);
                    return true;
                }
            }
            return false;
        }

        private static void MoveFigure(Figure sender, int nextRow, int nextColumn)
        {
            Figures[sender.Position.Row, sender.Position.Column] = null;
            Figures[nextRow, nextColumn] = sender;
            sender.MainViewModel.MoveTo(PointToPoint3D(GetTileCenter(nextRow, nextColumn)));
        }

        private static System.Windows.Media.Media3D.Point3D PointToPoint3D(Point p)
        {
            return new System.Windows.Media.Media3D.Point3D(p.X, p.Y, 0);
        }

        private static void TakeDamage()
        {
            int health = 0;
            foreach (Figure figure in _activeEnemyFigures)
                health += figure.Star;
            OnMatchEnd(health);
        }

        public static List<Figure> CheckForUpgrades(Figure newFigure, bool deletePrefabInternally)
        {
            int numberOfSameUnits = 1;
            Figure figure2 = null;
            foreach (Figure figure in AllFigures)
            {
                if (MatchManager.Instance.MatchState != Enums.MatchState.Preparation && figure.Position.Row != -1)
                    continue;

                if (figure.Star == newFigure.Star &&
                    figure.Unit.GetType().Name == newFigure.Unit.GetType().Name &&
                    figure.Piece.PieceType == newFigure.Piece.PieceType &&
                    figure != newFigure)
                    ++numberOfSameUnits;

                if (numberOfSameUnits == 3)
                {
                    figure.Upgrade(newFigure, figure2);

                    PrepareSacrifice(figure2, deletePrefabInternally);
                    PrepareSacrifice(newFigure, deletePrefabInternally);

                    List<Figure> sacrifices = new List<Figure>();
                    sacrifices.Add(figure2);
                    sacrifices.Add(newFigure);
                    sacrifices.AddRange(CheckForUpgrades(figure, deletePrefabInternally));

                    return sacrifices;
                }
                if (numberOfSameUnits == 2)
                    figure2 = figure;
            }
            return new List<Figure>();
        }

        private static void PrepareSacrifice(Figure figure, bool deletePrefab)
        {
            Figures[figure.Position.Row, figure.Position.Column] = null;
            figure.Untargetable = true;
            figure.MainViewModel.SetActive(false);
            if (_activeAllyFigures.Contains(figure))
                RemoveFromBoard(figure);

            if (deletePrefab)
                AllFigures.Remove(figure);
        }

        public static bool ImpaleToPosition(Figure newFigure, int x, int y)
        {
            if (Figures[x, y] != null)
                return false;

            newFigure.OnDeath += f =>
            {
                Figures[f.Position.Row, f.Position.Column] = null;
                _activeAllyFigures.Remove(f);
                if (_activeAllyFigures.Count == 0)
                {
                    int health = 0;
                    foreach (Figure figure in _activeEnemyFigures)
                    {
                        health += figure.Unit.Star;
                    }
                    OnMatchEnd(health);
                }
            };
            newFigure.OnMove += MoveFigure;
            newFigure.Owner = Owner;

            _activeAllyFigures.Add(newFigure);

            return true;
        }

        public static List<Figure> CopyActiveFigures()      //         trasform to package
        {
            List<Figure> copiedUnits = new List<Figure>();
            foreach (Figure figure in _activeAllyFigures)
            {
                //copiedUnits.Add(Instantiate(figure));
            }
            return copiedUnits;
        }

        public static void SpawnEnemyFigures(List<Figure> figures)
        {
            foreach (Figure figure in figures)
            {
                figure.CarryEnemyColors();
                figure.Position.Row = 7 - figure.Position.Row;
                figure.Position.Column = 7 - figure.Position.Column;
                Figures[figure.Position.Row, figure.Position.Column] = figure;
                figure.MainViewModel.MoveTo(PointToPoint3D(GetTileCenter(figure.Position.Row, figure.Position.Column)));
                _activeEnemyFigures.Add(figure);
                figure.OnDeath += f =>
                {
                    _activeEnemyFigures.Remove(f);
                    if (_activeEnemyFigures.Count == 0)
                        OnMatchEnd(0);
                };
                figure.OnMove += MoveFigure;
                figure.PrepareForBattle();
            }
            DPSmanager.Instance.SetEnemyFigures(_activeEnemyFigures);
        }

        public static void SellFigure(Figure figure)
        {
            AllFigures.Remove(figure);
            if (_activeAllyFigures.Contains(figure))
                _activeAllyFigures.Remove(figure);
            Figures[figure.Position.Row, figure.Position.Column] = null;
            UnitShop.SellUnit(figure);
        }

        public static void PrepareForBattle()
        {
            foreach (Figure figure in _activeAllyFigures)
                figure.PrepareForBattle();
        }

        public static void RecoverFromBattle()
        {
            foreach (Figure figure in AllFigures)
            {
                if (figure.Position.Row == -1)
                    break;
                figure.Restart();
            }
            SpawnAllChessFigures();
        }

        private static void SpawnChessFigure(int index, int row, int column)
        {
            // quarterion - for orientation, change if needed (default Quaternion.identity)
            Figures[row, column] = AllFigures[index];
            AllFigures[index].MainViewModel.MoveTo(PointToPoint3D(GetTileCenter(row, column)));
            Figures[row, column].Position.Row = row;
            Figures[row, column].Position.Column = column;
            if (row != -1)
                _activeAllyFigures.Add(AllFigures[index]);
        }

        private static void SpawnAllChessFigures()
        {
            int i = 0;
            foreach (Figure figure in AllFigures)
            {
                figure.MainViewModel.SetActive(true);
                SpawnChessFigure(i++, figure.Position.Row, figure.Position.Column);
            }
        }

        private static void Initialize()
        {
            _boardTiles = new BoardTile();
            SetUpTheTiles();

            Figures = new FigureGraph();

            _activeAllyFigures = new List<Figure>();
            _activeEnemyFigures = new List<Figure>();

            Dijkstra.SetGraph(Figures);

            MatchManager.Instance.OnStateChage += matchState =>
            {
                switch (matchState)
                {
                    case Enums.MatchState.Preparation:
                        RecoverFromBattle();
                        List<Figure> sacrifices = new List<Figure>();
                        foreach (Figure figure in AllFigures)
                            sacrifices.AddRange(CheckForUpgrades(figure, false));

                        foreach (Figure figure in sacrifices)
                            AllFigures.Remove(figure);

                        foreach (Figure figure in _activeEnemyFigures)
                            figure.Destroy();
                        break;
                    case Enums.MatchState.Disposal:
                        SellExcessFigures();
                        break;
                    case Enums.MatchState.Stretching:
                        PrepareForBattle();
                        break;
                    case Enums.MatchState.Battle:
                        if (_activeAllyFigures.Count == 0)
                            TakeDamage();
                        break;
                }
            };

            DPSmanager.Instance.SetAllyFigures(_activeAllyFigures);
            _pieceCounter = new PieceCounter();
        }

        public static void SellExcessFigures()
        {
            int[] cost = new int[5];
            foreach (Figure figure in _activeAllyFigures)
            {
                switch (figure.Piece.PieceType)
                {
                    case Enums.Piece.Pawn:
                        if (cost[(int)figure.Piece.PieceType] + figure.Cost > 8)
                        {
                            RemoveFromBoard(figure);
                            SellFigure(figure);
                            continue;
                        }
                        break;
                    case Enums.Piece.Bishop:
                        if (cost[(int)figure.Piece.PieceType] + figure.Cost > 6)
                        {
                            RemoveFromBoard(figure);
                            SellFigure(figure);
                            continue;
                        }
                        break;
                    case Enums.Piece.Knight:
                        if (cost[(int)figure.Piece.PieceType] + figure.Cost > 8)
                        {
                            RemoveFromBoard(figure);
                            SellFigure(figure);
                            continue;
                        }
                        break;
                    case Enums.Piece.Rook:
                        if (cost[(int)figure.Piece.PieceType] + figure.Cost > 10)
                        {
                            RemoveFromBoard(figure);
                            SellFigure(figure);
                            continue;
                        }
                        break;
                    case Enums.Piece.Queen:
                        if (cost[(int)figure.Piece.PieceType] + figure.Cost > 8)
                        {
                            RemoveFromBoard(figure);
                            SellFigure(figure);
                            continue;
                        }
                        break;
                }
                cost[(int)figure.Piece.PieceType] += figure.Cost;
            }
        }

        public static Point GetSelectedTile(HelixToolkit.Wpf.SharpDX.Model.Scene.SceneNode sceneNode)
        {
            for (int row = -1; row < _rows; row++)
            {
                for (int column = 0; column < _columns; column++)
                {
                    if (_boardTiles[row, column].MainViewModel.GroupModel.SceneNode == sceneNode)
                        return new Point(row, column);
                }
            }
            return null;
        }

        public static void MoveFigureToPoisition(Figure _selectedFigure, int row, int column)
        {
            Figures[_selectedFigure.Position.Row, _selectedFigure.Position.Column] = null;

            if (Figures[row, column] != null)
            {
                Figure figureToSwap = Figures[row, column];
                figureToSwap.MainViewModel.MoveTo(
                    PointToPoint3D(GetTileCenter(_selectedFigure.Position.Row, _selectedFigure.Position.Column)));
                Figures[_selectedFigure.Position.Row, _selectedFigure.Position.Column] = figureToSwap;

                AssignFigureToPosition(figureToSwap, _selectedFigure.Position.Row, _selectedFigure.Position.Column);
            }

            Figures[row, column] = _selectedFigure;
            AssignFigureToPosition(_selectedFigure, row, column);

            //boardTiles[selectionRow, selectionColumn].ChangeColor(BoardTile.DefaultColor);
        }

        private static void AssignFigureToPosition(Figure figure, int row, int column)
        {
            figure.MainViewModel.MoveTo(PointToPoint3D(GetTileCenter(row, column)));
            int previousRow = figure.Position.Row;
            figure.Position.Row = row;
            figure.Position.Column = column;
            figure.Untargetable = row == -1;

            if ((previousRow >= 0) != (row >= 0))
            {
                if (row == -1)
                {
                    RemoveFromBoard(figure);
                }
                else
                {
                    AddToBoard(figure);
                }
            }
        }

        private static void RemoveFromBoard(Figure figure)
        {
            BoardFigureScripts.Synergies.SynergyManager.RemoveFigure(figure);
            _activeAllyFigures.Remove(figure);
            _pieceCounter.UpdateCostSum(figure.Piece.PieceType, -figure.Cost);
        }

        private static void AddToBoard(Figure figure)
        {
            BoardFigureScripts.Synergies.SynergyManager.AddFigure(figure);
            _activeAllyFigures.Add(figure);
            _pieceCounter.UpdateCostSum(figure.Piece.PieceType, figure.Cost);
        }

        private static Point GetTileCenter(int x, int y)
        {
            return _boardTiles[x, y].GetTileCenter();
        }

        private static void SetUpTheTiles()
        {
            for (int row = -1; row < _rows; row++)
            {
                for (int column = 0; column < _columns; column++)
                {
                    _boardTiles[row, column] = new BoardTile(new Point(row, column), row + 1);
                }
            }
        }
    }
}
