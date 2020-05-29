using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetAnotherAutoChess.Business;

namespace YetAnotherAutoChess
{
    namespace Calculators
    {
        public static class RarityCalculator
        {
            public static Enums.Rarity CalculateRarity(int cost)
            {
                switch (cost)
                {
                    case 1:
                        return Enums.Rarity.Common;
                    case 2:
                    case 3:
                        return Enums.Rarity.Uncommon;
                    case 4:
                    case 5:
                        return Enums.Rarity.Rare;
                    case 6:
                    case 7:
                        return Enums.Rarity.Epic;
                    case 8:
                    case 9:
                    case 10:
                        return Enums.Rarity.Legendary;
                    default:
                        return Enums.Rarity.none;
                }
            }
        }

        public static class CostCalculator
        {
            public static int CalculateFinalCost(Unit unit, Enums.Piece piece)
            {
                return CalculateFinalCost(unit.Cost, piece);
            }

            public static int CalculateFinalCost(PlayerServiceReference.BaseUnitPackage unit, Enums.Piece piece)
            {
                return CalculateFinalCost(unit.Cost, piece);
            }

            private static int CalculateFinalCost(int cost, Enums.Piece piece)
            {
                switch (piece)
                {
                    case Enums.Piece.Pawn:
                        if (cost <= 8)
                            return cost;
                        return 0;
                    case Enums.Piece.Bishop:
                        if (cost <= 3)
                            return 3;
                        if (cost <= 6)
                            return 6;
                        return 0;
                    case Enums.Piece.Knight:
                        if (cost <= 4)
                            return 4;
                        if (cost <= 8)
                            return 8;
                        return 0;
                    case Enums.Piece.Rook:
                        if (cost <= 5)
                            return 5;
                        if (cost <= 10)
                            return 10;
                        return 0;
                    case Enums.Piece.Queen:
                        return 8;
                }
                return 0;
            }
        }
    }
}
