using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace YetAnotherAutoChess
{
    class UnitShopButtonsUI
    {
        public List<Button> Buttons { get; set; }
        public List<Button> DefaultPieceButtons { get; set; }
        private List<Grid> _pieceButtonGrids { get; set; }
        public List<Image> Images { get; set; }
        public Grid UnitGrid { get; set; }
        public Grid DefaultPieceGrid { get; set; }
        public Canvas MainCanvas { get; }
        private string absolutePath;
        public UnitShopButtonsUI(Canvas mainCanvas)
        {
            instantiateLists();
            MainCanvas = mainCanvas;
            MainCanvas.Children.Add(UnitGrid = (Application.Current.Resources["userMainGui"] as Grid));
            MainCanvas.Children.Add(DefaultPieceGrid = (Application.Current.Resources["DefaultPieceGrid"] as Grid));
            AddButtonsAndImages();
            DefaultPieceSelector.Initialize(DefaultPieceButtons);
            DefaultPieceSelector.OnDefaultPieceChange += onDefaultPieceChange;
        }
        private void instantiateLists()
        {
            Buttons = new List<Button>();
            _pieceButtonGrids = new List<Grid>() { null, null, null, null, null };
            Images = new List<Image>();
            DefaultPieceButtons = new List<Button>();
        }

        private void AddButtonsAndImages()
        {
            absolutePath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            foreach (UIElement uIElement in UnitGrid.Children)
            {
                if (uIElement is Button b)
                {
                    Buttons.Add(b);
                    b.Click += unitClicked;
                    if (b.Content is Image i)
                    {
                        Images.Add(i);
                    }
                }
            }

            int counter = 0;
            foreach (UIElement uIElement in DefaultPieceGrid.Children)
            {
                if (uIElement is Button b && uIElement.Uid == "Piece")
                {
                    Image i = b.Content as Image;
                    Uri uri = new Uri(absolutePath + "/PiecesV2/" + counter.ToString() + ".png", UriKind.Absolute);
                    i.Source = new System.Windows.Media.Imaging.BitmapImage(uri);
                    DefaultPieceButtons.Add(b);
                    counter++;

                }
            }
        }

        private void onDefaultPieceChange(Enums.Piece piece)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i != (int)piece - 1)
                {
                    DefaultPieceButtons[i].Background = System.Windows.Media.Brushes.White;
                }
            }
            if (piece != Enums.Piece.none)
            {
                DefaultPieceButtons[(int)piece - 1].Background = System.Windows.Media.Brushes.Aqua;
                removePieceButtonGrids();
            }
        }

        private void unitClicked(Object sender, RoutedEventArgs e)
        {
            if (DefaultPieceSelector.GetDefaultPiece() != Enums.Piece.none) return;
            int buttonIndex = Buttons.IndexOf(sender as Button);
            if (_pieceButtonGrids[buttonIndex] != null)
            {
                MainCanvas.Children.Remove(_pieceButtonGrids[buttonIndex]);
                _pieceButtonGrids[buttonIndex] = null;
                return;
            }
            _pieceButtonGrids[buttonIndex] = Application.Current.Resources["Unit" + buttonIndex.ToString()] as Grid;
            setSources(_pieceButtonGrids[buttonIndex]);
            MainCanvas.Children.Add(_pieceButtonGrids[buttonIndex]);
        }

        private void removePieceButtonGrids()
        {
            for (int i = _pieceButtonGrids.Count - 1; i >= 0; --i)
            {
                Grid temp = _pieceButtonGrids[i];
                _pieceButtonGrids[i] = null;
                MainCanvas.Children.Remove(temp);
            }
        }

        private void setSources(Grid grid)
        {
            int counter = 0;
            foreach (UIElement child in grid.Children)
            {
                if (child is Button b)
                {
                    Image i = b.Content as Image;
                    Uri uri = new Uri(absolutePath + "/PiecesV2/" + counter.ToString() + ".png", UriKind.Absolute);
                    i.Source = new System.Windows.Media.Imaging.BitmapImage(uri);
                    counter++;
                }
            }
        }
    }
}
