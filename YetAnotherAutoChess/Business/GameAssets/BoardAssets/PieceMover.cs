using HelixToolkit.Wpf.SharpDX.Model.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.GameAssets.BoardAssets
{
    public static class PieceMover
    {
        private static Figure _selectedFigure;
        private static Figure _openTooltipFigure;

        private static int _selectionRow;
        private static int _selectionColumn;
        private static BoardTile _lastTilePassed;

        private static System.Windows.Media.Color _selectColor = System.Windows.Media.Colors.Green;

        private static void ShowSelectedFigureTooltip(int row, int column)
        {
            if (Board.Figures[row, column] != null)
                _openTooltipFigure = Board.Figures[row, column];
            //_openTooltipFigure.FigureUIManager.ShowTooltip();
        }

        private static void HideSelectedFigureTooltip()
        {
            //if (_openTooltipFigure != null)
            //    _openTooltipFigure.FigureUIManager.HideTooltip();
        }

        public static void OnRightMouseButtonClick(SceneNode sceneNode)
        {
            UpdateSelection(sceneNode);
            HideSelectedFigureTooltip();
            ShowSelectedFigureTooltip(_selectionRow, _selectionColumn);
        }

        public static void OnLeftMouseButtonDown(SceneNode sceneNode)
        {
            HideSelectedFigureTooltip();

            if (MatchManager.Instance.MatchState != Enums.MatchState.Preparation)
                return;

            UpdateSelection(sceneNode);

            SelectFigure();
        }

        private static void SelectFigure()
        {
            if (_selectionColumn >= 0 && _selectionRow >= -1)
            {
                if (_selectedFigure == null)
                {
                    if(SelectFigure(_selectionRow, _selectionColumn))
                    {
                        _lastTilePassed = Board.GetSelectedBoardTile(_selectionRow, _selectionColumn);
                        _lastTilePassed.ChangeColor(_selectColor);
                    }
                }
            }
        }

        private static bool SelectFigure(int row, int column)
        {
            if (Board.Figures[row, column] == null)
                return false;

            _selectedFigure = Board.Figures[row, column];
            return true;
        }

        public static void OnLeftMouseButtonHold(SceneNode sceneNode)
        {
            UpdateSelection(sceneNode);
            MoveFigureWhileSelected();
        }

        private static void MoveFigureWhileSelected()
        {
            if (_selectedFigure == null)
                return;

            if (_selectionColumn >= 0 && _selectionRow >= -1)
            {
                BoardTile boardTile = Board.GetSelectedBoardTile(_selectionRow, _selectionColumn);

                if (_selectionRow > 3)
                {
                    _selectionRow = 3;
                }

                Point p = boardTile.GetTileCenter();
                _selectedFigure.MainViewModel.MoveTo(PointToPoint3D(p));

                if (boardTile != _lastTilePassed)
                {
                    _lastTilePassed.ChangeColor(BoardTile.DefaultColor);
                    _lastTilePassed = boardTile;
                    boardTile.ChangeColor(_selectColor);
                }
            }
        }

        public static System.Windows.Media.Media3D.Point3D PointToPoint3D(Point p)
        {
            return new System.Windows.Media.Media3D.Point3D(p.X, 0, p.Y);
        }

        public static void OnLeftMouseButtonUp(SceneNode sceneNode)
        {
            PlaceFigure();
        }

        private static void PlaceFigure()
        {
            if (_selectionColumn >= 0 && _selectionRow >= -1)
            {
                if (_selectionRow > 3)
                {
                    _selectionRow = 3;
                }
                if (_selectedFigure != null)
                {
                    Board.MoveFigureToPoisition(_selectedFigure, _selectionRow, _selectionColumn);
                    _lastTilePassed.ChangeColor(BoardTile.DefaultColor);
                }
            }
        }

        private static void UpdateSelection(SceneNode sceneNode)
        {
            Point p = Board.GetSelectedTile(sceneNode);
            _selectionRow = p.X;
            _selectionColumn = p.Y;
        }
    }
}
