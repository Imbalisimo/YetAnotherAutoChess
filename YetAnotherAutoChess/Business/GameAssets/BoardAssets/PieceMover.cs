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
                    SelectFigure(_selectionRow, _selectionColumn);
                    _lastTilePassed = Board.GetSelectedBoardTile(_selectionRow, _selectionColumn);
                    _lastTilePassed.ChangeColor(System.Windows.Media.Colors.AliceBlue);
                }
            }
        }

        private static void SelectFigure(int row, int column)
        {
            if (Board.Figures[row, column] == null)
                return;

            _selectedFigure = Board.Figures[row, column];
        }

        public static void OnLeftMouseButtonHold(SceneNode sceneNode)
        {
            UpdateSelection(sceneNode);
            MoveFigureWhileSelected();
        }

        private static void MoveFigureWhileSelected()
        {
            if (_selectionColumn >= 0 && _selectionRow >= -1)
            {
                BoardTile boardTile = Board.GetSelectedBoardTile(_selectionRow, _selectionColumn);
                //if (_selectedFigure != null)
                {
                    if (_selectionRow > 3)
                    {
                        _selectionRow = 3;
                    }
                    boardTile.ChangeColor(System.Windows.Media.Colors.LawnGreen);

                    Point p = boardTile.GetTileCenter();
                    _selectedFigure.MainViewModel.MoveTo(new System.Windows.Media.Media3D.Point3D(p.X, p.Y, 0));
                }
                if (boardTile != _lastTilePassed)
                {
                    _lastTilePassed.ChangeColor(BoardTile.DefaultColor);
                    _lastTilePassed = boardTile;
                }
            }
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
                    Board.MoveFigureToPoisition(_selectedFigure, _selectionRow, _selectionColumn);
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
