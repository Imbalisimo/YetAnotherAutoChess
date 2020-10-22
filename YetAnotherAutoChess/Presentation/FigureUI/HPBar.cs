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
            MainCanvas.instance.SizeChanged += CoordsChangeOnSizeChange;
            this.Height = 10;
            this.Width = 40;

            Move(positionX, positionY - 4);
            this.Visibility = System.Windows.Visibility.Visible;
            this.IsIndeterminate = false;
            this.Value = 100;
        }

        private void CoordsChangeOnSizeChange(object sender, SizeChangedEventArgs e)
        {
            Canvas canvas = sender as Canvas;

            SizeChangedEventArgs canvas_Changed_Args = e;

            if (canvas_Changed_Args.PreviousSize.Width == 0) return;


            double old_Height = canvas_Changed_Args.PreviousSize.Height;
            double new_Height = canvas_Changed_Args.NewSize.Height;
            double old_Width = canvas_Changed_Args.PreviousSize.Width;
            double new_Width = canvas_Changed_Args.NewSize.Width;

            
        }


        public void Move(double x, double y, int currentSizeX, int currentSizeY)
        {
            this.Margin = new System.Windows.Thickness(x/currentSizeX, 0, 0, (y-4)/currentSizeY);
        }

        public void Move(double x, double y)
        {
            this.Margin = new System.Windows.Thickness(x/4, 0, 0, (y - 4)/4);
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
