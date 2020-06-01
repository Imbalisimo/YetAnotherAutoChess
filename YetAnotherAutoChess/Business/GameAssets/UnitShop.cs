using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using YetAnotherAutoChess.PlayerServiceReference;
using YetAnotherAutoChess.Presentation;

namespace YetAnotherAutoChess.Business.GameAssets
{
    public static class UnitShop
    {
        internal static ShopUI shopUI;

        public static int NumberOfUnits { get => 5; }
        public static bool ShopLocked;
        private static List<BaseUnitPackage> units = new List<BaseUnitPackage>();

        public static void Initialize(Grid mainGrid)
        {
            shopUI = new ShopUI(new UnitShopButtonsUI(mainGrid));
            PlaceNewUnitsInShop();
        }

        public static void Reroll()
        {
            if (units == null)
                units = new List<BaseUnitPackage>();

            foreach (BaseUnitPackage unit in units)
            {
                ReturnUnitToPool(unit);
            }
            PlaceNewUnitsInShop();
        }

        public static void PlaceNewUnitsInShop()
        {
            units = PlayerClient.RequestRandomUnitsFromPool(MatchManager.Instance.Level, NumberOfUnits);
            if (units == null)
                return;
            shopUI.PlaceNewUnits(units);
        }

        public static bool BuyUnit(BaseUnitPackage unit, Enums.Piece piece)//To do
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

            List<BaseUnitPackage> units = FigureManager.Disassemble(figure);
            foreach (BaseUnitPackage unit in units)
            {
                ReturnUnitToPool(unit);
            }
        }

        public static void ReturnUnitToPool(BaseUnitPackage unit)
        {
            PlayerClient.ReturnUnitToPool(unit);
        }
    }
}
