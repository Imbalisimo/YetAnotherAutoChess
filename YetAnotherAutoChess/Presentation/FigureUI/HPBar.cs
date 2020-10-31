using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace YetAnotherAutoChess.Presentation.FigureUI
{
    class HPBar : ProgressBar, IFigureUI
    {
        public HPBar(double positionX, double positionY)
        {
            MainCanvas.instance.Children.Add(this);
            this.Height = 10;
            this.Width = 40;

            Move(positionX, positionY - 4);
            this.Visibility = System.Windows.Visibility.Visible;
            this.IsIndeterminate = false;
            this.Value = 100;
        }

        public void Move(double x, double y)
        {
            this.Margin = new System.Windows.Thickness(x/8 + 191.1, 0, 0,(y - 30)/7.15 - 470);
            Console.WriteLine("Coord X: " + x);
            Console.WriteLine("Coord Y: " + y);
        }

        public void SetMana(int mana)
        {

        }

        public void SetHealth(int health)
        {
            this.Value = health;
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
            this.Foreground = System.Windows.Media.Brushes.Red;
        }
    }
}
