using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Presentation.FigureUI
{
    class FigureUIManager : IFigureUI
    {
        public FigureUIManager()
        {
            figureUIBars = new FigureUIBars(0,0);
            rankImage = new RankImage(0, 0);
        }

        IFigureUI figureUIBars;
        IFigureUI rankImage;
        public void Move(double x, double y)
        {
            figureUIBars.Move(x, y);
            rankImage.Move(x, y);
        }

        public void SetMana(int manaToSubtract)
        {
            figureUIBars.SetMana(manaToSubtract);
            rankImage.SetMana(manaToSubtract);
        }

        public void SetHealth(int damage)
        {
            figureUIBars.SetHealth(damage);
            rankImage.SetHealth(damage);
        }

        public void UpgradeToTwoStar()
        {
            figureUIBars.UpgradeToTwoStar();
            rankImage.UpgradeToTwoStar();
        }

        public void UpgradeToThreeStar()
        {
            figureUIBars.UpgradeToThreeStar();
            rankImage.UpgradeToThreeStar();
        }
        public void SetActive(bool value)
        {
            figureUIBars.SetActive(value);
            rankImage.SetActive(value);
        }

        public void CarryEnemyColors()
        {
            figureUIBars.CarryEnemyColors();
            rankImage.CarryEnemyColors();
        }
    }
}
