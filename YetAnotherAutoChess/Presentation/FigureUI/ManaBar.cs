﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace YetAnotherAutoChess.Presentation.FigureUI
{
    class ManaBar : ProgressBar, IFigureUI
    {
        public ManaBar(double positionX, double positionY)
        {
            this.Foreground = System.Windows.Media.Brushes.Blue;
            this.Height = 10;
            this.Width = 50;
            Move(positionX, positionY + 4);
        }
        public void Move(double x, double y)
        {
            this.Margin = new System.Windows.Thickness(x, y + 4, 0, 0);
        }

        public void SetMana(int mana)
        {
            this.Value = mana;
        }

        public void SetHealth(int damage)
        {

        }

        public void UpgradeToThreeStar()
        {

        }

        public void UpgradeToTwoStar()
        {

        }

        public void SetActive(bool value)
        {
            this.Visibility = value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
        }

        public void CarryEnemyColors()
        {

        }
    }
}
