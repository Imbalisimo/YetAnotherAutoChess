using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace YetAnotherAutoChess.Presentation.FigureUI
{
    class RankImage : Image, IFigureUI
    {
        private String absolutePath;
        public RankImage(double positionX, double positionY)
        {
            MainCanvas.instance.Children.Add(this);
            absolutePath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            this.Width = 17;
            this.Height = 17;
            Move(positionX + 40, positionY);
            this.Source = new System.Windows.Media.Imaging.
                    BitmapImage(new Uri(absolutePath + "/Images/UIstuff/Stars/bronze.png", UriKind.Absolute));
        }
        public void Move(double x, double y)
        {
            this.Margin = new System.Windows.Thickness(x / 8 + 192 + 40, 0, 0, y / 7.15 - 470);
        }

        public void SetMana(int manaToSubtract)
        {

        }

        public void SetHealth(int damage)
        {

        }

        public void UpgradeToTwoStar()
        {
            this.Source = new System.Windows.Media.Imaging.
                    BitmapImage(new Uri(absolutePath + "/Images/UIstuff/Stars/silver.png", UriKind.Absolute));
        }

        public void UpgradeToThreeStar()
        {
            this.Source = new System.Windows.Media.Imaging.
                    BitmapImage(new Uri(absolutePath + "/Images/UIstuff/Stars/gold.png", UriKind.Absolute));
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
