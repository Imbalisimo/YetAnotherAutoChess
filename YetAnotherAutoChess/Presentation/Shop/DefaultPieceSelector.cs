using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace YetAnotherAutoChess
{

    static class DefaultPieceSelector
    {
        public delegate void DefaultPieceChange(Enums.Piece piece);
        public static event DefaultPieceChange OnDefaultPieceChange;

        private static Enums.Piece _defaultPiece;
        public static void Initialize(List<Button> buttons)
        {
            _defaultPiece = Enums.Piece.none;
            foreach (Button button in buttons)
            {
                button.Click += DefaultPieceClicked;
            }
        }

        public static void DefaultPieceClicked(Object sender, RoutedEventArgs e)
        {
            Enums.Piece piece = (Enums.Piece) (sender as Button).DataContext;

            if (piece == _defaultPiece)
            {
                _defaultPiece = Enums.Piece.none;
            }
            else
            {

                _defaultPiece = piece;
            }
            OnDefaultPieceChange(_defaultPiece);
        }

        public static Enums.Piece GetDefaultPiece()
        {
            return _defaultPiece;
        }

    }
}