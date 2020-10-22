using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Presentation.FigureUI
{
    class FigureUIBars : IFigureUI
    {
        IFigureUI hpBar;
        IFigureUI manaBar;

        public FigureUIBars(double positionX, double positionY)
        {
            hpBar = new HPBar(positionX, positionY);
            manaBar = new ManaBar(positionX, positionY);
        }

        public void Move(double x, double y)
        {
            hpBar.Move(x,y);
            manaBar.Move(x, y);
        }

        public void SetMana(int manaToSubtract)
        {
            hpBar.SetMana(manaToSubtract);
            manaBar.SetMana(manaToSubtract);
        }

        public void SetHealth(int damage)
        {
            hpBar.SetHealth(damage);
            manaBar.SetHealth(damage);
        }

        public void UpgradeToThreeStar()
        {

        }

        public void UpgradeToTwoStar()
        {

        }

        public void SetActive(bool value)
        {
            hpBar.SetActive(value);
            manaBar.SetActive(value);
        }

        public void CarryEnemyColors()
        {
            hpBar.CarryEnemyColors();
            manaBar.CarryEnemyColors();
        }
    }
}
