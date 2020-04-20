using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using YetAnotherAutoChess.Business;
using YetAnotherAutoChess.Business.GameAssets;

namespace YetAnotherAutoChess.Presentation
{
    class ShopUI
    {
        public List<Image> Images = new List<Image>();
        public GameObject DefaultPiece;
        private UnitShopButtonsUI _unitShopButtonsUI;
        public Button BtnLock;
        Image Locked;
        Image Unlocked;

        public ShopUI(UnitShopButtonsUI unitShopButtonsUI)
        {
            _unitShopButtonsUI = unitShopButtonsUI;
            MatchManager.Instance.OnStateChage += state => {
                if (state == Enums.MatchState.Preparation && UnitShop.ShopLocked)
                    ToggleLock(this, new RoutedEventArgs());
            };
            AddButtonEvents();
            AddImageSources();
            _unitShopButtonsUI.OnUnitBuyAttempt += buyAttempt;
            _unitShopButtonsUI.OnRerollClick += (o) => { Reroll(o, new RoutedEventArgs()); };
        }

        public void buyAttempt(Grid grid, Button button, Unit unit, Enums.Piece piece)
        {
            if (!UnitShop.BuyUnit(unit as Unit, piece))
            {
                return;
            }
            _unitShopButtonsUI.DisableGridAndButton(grid,button);
        }

        public void PlaceNewUnits(dynamic units)//to do
        {
            for(int i=0; i<5; i++)
            {
                _unitShopButtonsUI.UnitButtons[i].DataContext = units[i];
                (_unitShopButtonsUI.UnitButtons[i].Content as Image).Source = new System.Windows.Media.Imaging.
                    BitmapImage(new Uri(_unitShopButtonsUI.getAbsolutePath() + "/Images/Units/" + units[i].Name + ".jpg", UriKind.Absolute));
            }
        }
        private void AddButtonEvents()//Na svaki UnitButton doda da kad se klikne kupimo unita
        {
            BtnLock =  _unitShopButtonsUI.getLockButton();
            BtnLock.Click += ToggleLock;
            foreach (Button button in _unitShopButtonsUI.UnitButtons)
            {
                button.Click += (o, e) =>
                {
                    BuyUnitDef(o as Button, (Unit)(o as Button).DataContext, DefaultPieceSelector.GetDefaultPiece());
                };
            }
        }

        public void BuyUnitDef(Button button, GameObject unit, Enums.Piece piece)//Buy unit if default piece is set
        {
            if (DefaultPieceSelector.GetDefaultPiece() == Enums.Piece.none)
                return;
            if (!UnitShop.BuyUnit(unit as Unit, piece))
            {
                return;
            }
            button.Content = null;
            button.IsEnabled = false;
        }

        private void AddImageSources() //just setting image sources, no need to be looked at
        {
            Locked = new Image();
            Unlocked = new Image();
            Uri uri = new Uri(_unitShopButtonsUI.getAbsolutePath() + "/Images/Buttons/Lock/Lock_locked.jpg", UriKind.Absolute);
            Locked.Source = new System.Windows.Media.Imaging.BitmapImage(uri);
            uri = new Uri(_unitShopButtonsUI.getAbsolutePath() + "/Images/Buttons/Lock/Lock_unlocked.jpg", UriKind.Absolute);
            Unlocked.Source = new System.Windows.Media.Imaging.BitmapImage(uri);
        }


        public void Reroll(object sender, RoutedEventArgs e)
        {
            UnitShop.Reroll();
            if (UnitShop.ShopLocked)
                ToggleLock(sender, e);
        }

        public void ToggleLock(object sender, RoutedEventArgs e)
        {
            UnitShop.ShopLocked = !UnitShop.ShopLocked;
            BtnLock.Content = UnitShop.ShopLocked ? Locked : Unlocked;
        }
    }
}
