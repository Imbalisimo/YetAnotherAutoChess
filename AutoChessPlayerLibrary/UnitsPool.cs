using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoChessPlayerLibrary
{
    public static class UnitsPool
    {
        #region DeclaringRandAndLists
        private static System.Random rand = new System.Random(System.Guid.NewGuid().GetHashCode());
        private static List<BaseUnitPackage> Common = new List<BaseUnitPackage>();
        private static List<BaseUnitPackage> UnCommon = new List<BaseUnitPackage>();
        private static List<BaseUnitPackage> Rare = new List<BaseUnitPackage>();
        private static List<BaseUnitPackage> Epic = new List<BaseUnitPackage>();
        private static List<BaseUnitPackage> Legendary = new List<BaseUnitPackage>();

        private static List<List<BaseUnitPackage>> UnitTypes = new List<List<BaseUnitPackage>> { Common, UnCommon, Rare, Epic, Legendary };
        #endregion

        #region UnitsGatheringMatrix
        private static int[,] UnitsGatheringMatrix = {
//     Common | UnCommon | Rare  | Epic  | Legendary
        {100,      0,        0,      0,     0 },                //Level 1
        {70,     100,        0,      0,     0 },                //Level 2
        {60,      95,      100,      0,     0 },                //Level 3
        {50,      85,      100,      0,     0 },                //Level 4
        {40,      75,       98,     100,    0 },                //Level 5
        {33,      63,       93,     100,    0 },                //Level 6
        {30,      60,       90,     100,    0 },                //Level 7
        {24,      54,       84,      99,    100  },             //Level 8
        {22,      52,       77,      97,    100  },             //Level 9
        {19,      44,       69,      94,    100  },             //Level 10

        };

        #endregion

        private static bool _initialized = false;

        public static List<BaseUnitPackage> GenerateUnits(int level, int count)
        {
            if (!_initialized)
            {
                Initialze();
                _initialized = true;
            }

            List<BaseUnitPackage> shopIntendedUnits = new List<BaseUnitPackage>();
            for (int i = 0; i < count; i++)
            {
                shopIntendedUnits.Add(GetUnit(rand.Next(100), level));
            }

            foreach (BaseUnitPackage unit in shopIntendedUnits)
            {
                if (unit == null)
                {
                    return null;
                }
            }
            return shopIntendedUnits;
        }

        public static BaseUnitPackage GetUnit(int RandomNumber, int level)
        {
            List<BaseUnitPackage> ListOfUnits = null;

            //Point ListOfUnits to a certain list to aquire units from depending on a level
            for (int i = 0; i < 5; i++)
            {
                if (RandomNumber <= UnitsGatheringMatrix[level - 1, i])
                {
                    ListOfUnits = UnitTypes[i];
                    break;
                }
            }

            //If it didn't come to an error take a unit from the list
            if (ListOfUnits != null && ListOfUnits.Count != 0)
            {
                RandomNumber = rand.Next(ListOfUnits.Count);
                BaseUnitPackage Unit = ListOfUnits[RandomNumber];
                ListOfUnits.RemoveAt(RandomNumber);
                return Unit;
            }
            //if list is empty get another unit via recursion
            else if (ListOfUnits.Count == 0 || ListOfUnits == null)
            {
                return GetUnitsV2(new List<bool>() { true, true, true, true, true }, RandomNumber, level);
                //return GetUnit(rand.Next(0,99), level);
            }
            return null;
        }

        private static BaseUnitPackage GetUnitsV2(List<bool> boolLevel, int randomNumber, int level)
        {
            if (UnitTypes.Count == 0) return null;

            int randomMax = 0;

            for (int i = 0; i < 5; i++)
            {
                if (UnitTypes[i].Count == 0)
                {
                    boolLevel[i] = false;
                }
                else
                {
                    randomMax += UnitsGatheringMatrix[level - 1, i];
                    if (i != 0)
                    {
                        randomMax -= UnitsGatheringMatrix[level - 1, i - 1];
                    }
                }
            }

            randomNumber = rand.Next(randomMax);

            int listSpan = 0;

            for (int i = 0; i < 5; i++)
            {
                if (boolLevel[i] == true)
                {
                    listSpan += UnitsGatheringMatrix[level - 1, i];
                    if (i != 0)
                    {
                        listSpan -= UnitsGatheringMatrix[level - 1, i - 1];
                        if (randomNumber <= listSpan)
                        {
                            List<BaseUnitPackage> ListOfUnits = UnitTypes[i];
                            randomNumber = rand.Next(ListOfUnits.Count - 1);
                            return ListOfUnits[randomNumber];
                        }
                        continue;
                    }

                    if (randomNumber <= UnitsGatheringMatrix[level - 1, 0])
                    {
                        List<BaseUnitPackage> ListOfUnits = UnitTypes[0];
                        randomNumber = rand.Next(ListOfUnits.Count - 1);
                        return ListOfUnits[randomNumber];
                    }
                }
            }
            return null;
        }

        public static void PutUnitInPool(BaseUnitPackage unit)
        {
            int rarity = 0;
            switch (unit.Cost)
            {
                case 1:
                    rarity = 0;
                    break;
                case 2:
                case 3:
                    rarity = 1;
                    break;
                case 4:
                case 5:
                    rarity = 2;
                    break;
                case 6:
                case 7:
                    rarity = 3;
                    break;
                default:
                    rarity = 4;
                    break;
            }
            UnitTypes[rarity].Add(unit);
        }

        private static void Initialze()
        {
            UnitTypes[0] = new List<BaseUnitPackage>();
            UnitTypes[1] = new List<BaseUnitPackage>();
            UnitTypes[2] = new List<BaseUnitPackage>();
            UnitTypes[3] = new List<BaseUnitPackage>();
            UnitTypes[4] = new List<BaseUnitPackage>();
            InitializePool();
        }

        private static void InitializePool()
        {
            List<BaseUnitPackage> units = new List<BaseUnitPackage>();
            units.Add(new BaseUnitPackage("Thor", 1));
            // Add more units

            foreach (BaseUnitPackage unit in units)
            {
                InitializeUnitsOf(unit);
            }
        }

        private static void InitializeUnitsOf(BaseUnitPackage unit)
        {
            int numCopies = 0;
            switch (unit.Cost)
            {
                case 1:
                    numCopies = 45;
                    break;
                case 2:
                case 3:
                    numCopies = 30;
                    break;
                case 4:
                case 5:
                    numCopies = 25;
                    break;
                case 6:
                case 7:
                    numCopies = 15;
                    break;
                default:
                    numCopies = 10;
                    break;
            }
            for (int i = 1; i < numCopies; ++i)
            {
                PutUnitInPool(unit.Clone());
            }
            PutUnitInPool(unit);
        }
    }
}
