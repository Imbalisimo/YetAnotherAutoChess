using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using YetAnotherAutoChess.Business;
using YetAnotherAutoChess.Business.GameAssets;

namespace YetAnotherAutoChess
{
    class UnitShopButtonsUI
    {
        public List<Button> UnitButtons { get; set; }
        public List<Button> DefaultPieceButtons { get; set; }
        private List<Grid> _pieceButtonGrids { get; set; }
        public List<Image> Images { get; set; }
        public Grid UnitGrid { get; set; }
        public Grid DefaultPieceGrid { get; set; }
        public Grid MainGrid { get; }
        private string absolutePath;
        private Button LockButton;

        public delegate void UnitBuyAttempt(Grid grid, Button button, PlayerServiceReference.BaseUnitPackage unit, Enums.Piece piece);
        public event UnitBuyAttempt OnUnitBuyAttempt;

        public delegate void onRerollAttempt(Button button);
        public event onRerollAttempt OnRerollClick;
        public UnitShopButtonsUI(Grid mainGrid)
        {
            instantiateLists();
            MainGrid = mainGrid;
            MainGrid.Children.Add(UnitGrid = (Application.Current.Resources["userMainGui"] as Grid));
            MainGrid.Children.Add(DefaultPieceGrid = (Application.Current.Resources["DefaultPieceGrid"] as Grid));
            AddButtonsAndImages();
            DefaultPieceSelector.Initialize(DefaultPieceButtons);
            DefaultPieceSelector.OnDefaultPieceChange += onDefaultPieceChange;
        }
        private void instantiateLists()
        {
            UnitButtons = new List<Button>();
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
                    UnitButtons.Add(b);
                    b.Click += unitClicked;
                    //if (b.Content is Image i)
                    //{
                    //    Images.Add(i);//Ne mislim da je vazno, svakom od UnitButton.Context uvek mozemo pristupiti
                    //}
                }
            }

            int counter = 0;
            Uri uri;
            foreach (UIElement uIElement in DefaultPieceGrid.Children)
            {
                if (uIElement is Button b)
                {
                    if (uIElement.Uid == "Piece")
                    {
                        Image i = b.Content as Image;
                        uri = new Uri(absolutePath + "/PiecesV2/" + counter.ToString() + ".png", UriKind.Absolute);
                        i.Source = new System.Windows.Media.Imaging.BitmapImage(uri);
                        DefaultPieceButtons.Add(b);
                        b.DataContext = counter + 1;
                        counter++;
                    }
                    else if (uIElement.Uid == "Lock")
                    {
                        LockButton = b;
                        uri = new Uri(absolutePath + "/Images/Buttons/Lock/Lock_unlocked.jpg", UriKind.Absolute);
                        (LockButton.Content as Image).Source = new System.Windows.Media.Imaging.BitmapImage(uri);
                    }
                    else if (uIElement.Uid == "Reroll")
                    {
                        b.Click += (o, e) =>
                        {
                            OnRerollClick(b);
                        };
                    }
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
            int buttonIndex = UnitButtons.IndexOf(sender as Button);
            if (_pieceButtonGrids[buttonIndex] != null)
            {
                MainGrid.Children.Remove(_pieceButtonGrids[buttonIndex]);
                _pieceButtonGrids[buttonIndex] = null;
                return;
            }
            _pieceButtonGrids[buttonIndex] = Application.Current.Resources["Unit" + buttonIndex.ToString()] as Grid;
            setSources(sender as Button,_pieceButtonGrids[buttonIndex]);
            MainGrid.Children.Add(_pieceButtonGrids[buttonIndex]);
        }

        private void removePieceButtonGrids()
        {
            for (int i = _pieceButtonGrids.Count - 1; i >= 0; --i)
            {
                Grid temp = _pieceButtonGrids[i];
                _pieceButtonGrids[i] = null;
                MainGrid.Children.Remove(temp);
            }
        }

        private void setSources(Button parentButton,Grid grid)
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
                    b.DataContext = parentButton.DataContext;
                    b.Click += (o, e) => { UnitWantsToBeBought(grid, parentButton, (PlayerServiceReference.BaseUnitPackage)parentButton.DataContext, (Enums.Piece)counter + 1); };
                }
            }
        }
        public void UnitWantsToBeBought(Grid grid, Button button, PlayerServiceReference.BaseUnitPackage unit, Enums.Piece piece)
        {
            OnUnitBuyAttempt(grid,button, unit, piece);
        }

        public void DisableGridAndButton(Grid grid, Button button)
        {
            MainGrid.Children.Remove(grid);
            (button.Content as Image).Source = null;
            button.IsEnabled = false;
        }

        public string getAbsolutePath()
        {
            return absolutePath;
        }
        public Button getLockButton()
        {
            return LockButton;
        }
    }
}
