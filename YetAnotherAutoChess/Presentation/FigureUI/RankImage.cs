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
        public RankImage(double positionX, double positionY)
        {
            this.Width = 17;
            this.Height = 17;
            Move(positionX + 40, positionY);
            this.Source = new System.Windows.Media.Imaging.
                    BitmapImage(new Uri("/Images/UIstuff/Stars/bronze.png", UriKind.Relative));
        }
        public void Move(double x, double y)
        {
            this.Margin = new System.Windows.Thickness((x + 40)/4, 0, 0, y/4);
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
                    BitmapImage(new Uri("./Images/UIstuff/Stars/silver.png", UriKind.Relative));
        }

        public void UpgradeToThreeStar()
        {
            this.Source = new System.Windows.Media.Imaging.
                    BitmapImage(new Uri("./Images/UIstuff/Stars/gold.png", UriKind.Relative)); //Mozda bude problem sa relative path,
                    //TODO testirati na vise nacina
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
