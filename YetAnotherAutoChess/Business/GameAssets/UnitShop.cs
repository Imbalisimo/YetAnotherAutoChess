using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using YetAnotherAutoChess.Presentation;

namespace YetAnotherAutoChess.Business.GameAssets
{
    public static class UnitShop
    {
        internal static ShopUI shopUI;

        public static bool ShopLocked;
        public static void Initialize(Grid mainGrid)
        {
            shopUI = new ShopUI(new UnitShopButtonsUI(mainGrid));
        }
        public static void Reroll()
        {
            if (units == null)
                units = new List<Unit>();

            foreach (GameObject unit in units)
            {
                ReturnUnitToPool(unit);
            }
            PlaceNewUnitsInShop();
        }

        private static List<Unit> units = new List<Unit>();
        public static void PlaceNewUnitsInShop()
        {
            //units = UnitsPool.GenerateUnits(MatchManager.Instance.Level);
            if (units == null)
                return;
            shopUI.PlaceNewUnits(units);
        }

        public static bool BuyUnit(Unit unit, Enums.Piece piece)//To do
        {
            Player player = Player.Instance;
            int cost = Calculators.CostCalculator.CalculateFinalCost(unit, piece);
            if (player.Pawns < cost)
                return false;

            if (!Board.SpawnFigure(unit, piece))
                return false;
            player.Pawns -= cost;
            units.Remove(unit);
            return true;
        }

        public static void SellUnit(Figure figure)
        {
            if (MatchManager.Instance.MatchState != Enums.MatchState.Preparation)
                return;
            Player player = Player.Instance;
            player.Pawns += figure.Cost;

            FigureManager.Disassemble(figure);
            foreach (GameObject unit in units)
            {
                ReturnUnitToPool(unit);
            }
        }

        public static void ReturnUnitToPool(GameObject unit)
        {
            //UnitsPool.PutUnitInPool(unit);
        }
    }
}
