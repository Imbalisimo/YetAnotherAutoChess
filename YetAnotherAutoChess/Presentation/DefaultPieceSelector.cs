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
            foreach (Button button in buttons)
            {
                button.Click += DefaultPieceClicked;
            }
        }

        public static void DefaultPieceClicked(Object sender, RoutedEventArgs e)
        {
            Enums.Piece piece;

            switch ((sender as Button).Name)
            {
                case "Pawn":
                    piece = Enums.Piece.Pawn;
                    break;
                case "Bishop":
                    piece = Enums.Piece.Bishop;
                    break;
                case "Knight":
                    piece = Enums.Piece.Knight;
                    break;
                case "Rook":
                    piece = Enums.Piece.Rook;
                    break;
                case "Queen":
                    piece = Enums.Piece.Queen;
                    break;
                default:
                    piece = Enums.Piece.none;
                    break;
            }
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