using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Presentation.FigureUI
{
    public interface IFigureUI
    {
        void Move(double x, double y);

        void SetHealth(int health);
        void SetMana(int mana);

        void UpgradeToTwoStar();

        void UpgradeToThreeStar();

        void SetActive(bool value);

        void CarryEnemyColors();
    }
}
