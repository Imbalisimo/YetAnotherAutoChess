using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetAnotherAutoChess.Business.GameAssets;

namespace YetAnotherAutoChess.Business.BoardFigureScripts.Synergies
{
    public static class SynergyManager
    {
        internal static List<Synergy> Synergies;
        public static List<List<Figure>> FiguresInSynergy;

        private static bool _initialized = false;

        public static void AddFigure(Figure figure)
        {
            if (!_initialized)
            {
                Initialize();
                _initialized = true;
            }

            foreach (Enums.Synergy synergy in figure.Unit.Stats.Synergies)
            {
                FiguresInSynergy[(int)synergy].Add(figure);
            }
        }

        public static void RemoveFigure(Figure figure)
        {
            foreach (Enums.Synergy synergy in figure.Unit.Stats.Synergies)
            {
                FiguresInSynergy[(int)synergy].Remove(figure);
            }
        }

        public static List<Buff> GetBuffsForFigure(Figure figure)
        {
            List<Buff> buffs = new List<Buff>();
            // Get blessings that apply to all allies
            foreach (Synergy synergy in Synergies)
            {
                if (synergy.SynergyBuffTarget == Enums.SynergyBuffTarget.AllAllies)
                {
                    int cost = 0;
                    foreach (Figure f in FiguresInSynergy[(int)synergy.SynergyName])
                        cost += f.Cost;
                    Buff buff = Synergies[(int)synergy.SynergyName].GrantBlessing(cost);
                    if (buff != null)
                        buffs.Add(buff);
                }
            }

            // Get blessings that apply to synergy holders
            foreach (Enums.Synergy synergy in figure.Unit.Stats.Synergies)
            {
                if (Synergies[(int)synergy].SynergyBuffTarget == Enums.SynergyBuffTarget.AllAllies)
                    continue;

                int cost = 0;
                foreach (Figure f in FiguresInSynergy[(int)synergy])
                    cost += f.Cost;
                Buff buff = Synergies[(int)synergy].GrantBlessing(cost);
                if (buff != null)
                    buffs.Add(buff);
            }
            return buffs;
        }

        public static List<Figure> FiguresWithSameSynergies(Figure figure)
        {
            List<Figure> figures = new List<Figure>();
            foreach (Enums.Synergy synergy in figure.Unit.Stats.Synergies)
            {
                figures.AddRange(FiguresInSynergy[(int)synergy]);
            }
            return figures;
        }

        public static void Initialize()
        {
            MatchManager.Instance.OnStateChage += BlessAllUnits;
            Synergies = new List<Synergy>(System.Enum.GetValues(typeof(Enums.Synergy)).Length);
            //foreach (Enums.Synergy synergy in System.Enum.GetValues(typeof(Enums.Synergy)))
            //    Synergies.Add(null);
            FiguresInSynergy = new List<List<Figure>>(System.Enum.GetValues(typeof(Enums.Synergy)).Length);
            //foreach (Enums.Synergy synergy in System.Enum.GetValues(typeof(Enums.Synergy)))
            //    FiguresInSynergy.Add(new List<Figure>());

            CreateSynergies();
            Synergies.RemoveAll(synergy => synergy == null);
        }

        private static void CreateSynergies()
        {
            CreateMythologies();
            CreateDeities();
        }

        private static void CreateMythologies()
        {
            Synergy Aztec = CreateSynergy(Enums.Synergy.Aztec, Enums.SynergyType.Mythology, Enums.SynergyBuffTarget.SynergyHolders,
                13);
            Synergy Celtic = CreateSynergy(Enums.Synergy.Celtic, Enums.SynergyType.Mythology, Enums.SynergyBuffTarget.SynergyHolders,
                8, 16, 24);
            Synergy Chinese = CreateSynergy(Enums.Synergy.Chinese, Enums.SynergyType.Mythology, Enums.SynergyBuffTarget.SynergyHolders,
                13);
            Synergy Egyptian = CreateSynergy(Enums.Synergy.Egyptian, Enums.SynergyType.Mythology, Enums.SynergyBuffTarget.SynergyHolders,
                8, 16, 24);
            Synergy Greek = CreateSynergy(Enums.Synergy.Greek, Enums.SynergyType.Mythology, Enums.SynergyBuffTarget.SynergyHolders,
                8, 16, 24);
            Synergy Hindu = CreateSynergy(Enums.Synergy.Hindu, Enums.SynergyType.Mythology, Enums.SynergyBuffTarget.SynergyHolders,
                8, 16, 24);
            Synergy Japanese = CreateSynergy(Enums.Synergy.Japanese, Enums.SynergyType.Mythology, Enums.SynergyBuffTarget.SynergyHolders,
                8, 24);
            Synergy Maya = CreateSynergy(Enums.Synergy.Maya, Enums.SynergyType.Mythology, Enums.SynergyBuffTarget.SynergyHolders,
                8, 16, 24);
            Synergy Norse = CreateSynergy(Enums.Synergy.Norse, Enums.SynergyType.Mythology, Enums.SynergyBuffTarget.SynergyHolders,
                13);
            Synergy Sumerian = CreateSynergy(Enums.Synergy.Sumerian, Enums.SynergyType.Mythology, Enums.SynergyBuffTarget.SynergyHolders,
                8, 16, 24);
            Synergy Voodoo = CreateSynergy(Enums.Synergy.Voodoo, Enums.SynergyType.Mythology, Enums.SynergyBuffTarget.SynergyHolders,
                13);
        }

        private static void CreateDeities()
        {
            Synergy Beauty = CreateSynergy(Enums.Synergy.Beauty, Enums.SynergyType.Deity, Enums.SynergyBuffTarget.AllAllies,
                8, 16, 24);
            Synergy Chief = CreateSynergy(Enums.Synergy.Chief, Enums.SynergyType.Deity, Enums.SynergyBuffTarget.SynergyHolders,
                8, 16, 24);
            Synergy Dragon = CreateSynergy(Enums.Synergy.Dragon, Enums.SynergyType.Deity, Enums.SynergyBuffTarget.SynergyHolders,
                8, 9, 39);
            Synergy Earth = CreateSynergy(Enums.Synergy.Earth, Enums.SynergyType.Deity, Enums.SynergyBuffTarget.SynergyHolders,
                8, 16);
            Synergy Harvest = CreateSynergy(Enums.Synergy.Harvest, Enums.SynergyType.Deity, Enums.SynergyBuffTarget.SynergyHolders,
                8, 16, 24);
            Synergy Hunt = CreateSynergy(Enums.Synergy.Hunt, Enums.SynergyType.Deity, Enums.SynergyBuffTarget.SynergyHolders,
                8, 16);
            Synergy Thunder = CreateSynergy(Enums.Synergy.Thunder, Enums.SynergyType.Deity, Enums.SynergyBuffTarget.SynergyHolders,
                8, 16, 24);
            Synergy Trickster = CreateSynergy(Enums.Synergy.Trickster, Enums.SynergyType.Deity, Enums.SynergyBuffTarget.SynergyHolders,
                1);
            Synergy Underworld = CreateSynergy(Enums.Synergy.Underworld, Enums.SynergyType.Deity, Enums.SynergyBuffTarget.AllAllies,
                13, 26);
            Synergy War = CreateSynergy(Enums.Synergy.War, Enums.SynergyType.Deity, Enums.SynergyBuffTarget.SynergyHolders,
                8, 16, 24);
            Synergy Water = CreateSynergy(Enums.Synergy.Water, Enums.SynergyType.Deity, Enums.SynergyBuffTarget.SynergyHolders,
                8, 16, 24);
            Synergy Wisdom = CreateSynergy(Enums.Synergy.Wisdom, Enums.SynergyType.Deity, Enums.SynergyBuffTarget.AllAllies,
                8, 16, 24);
        }

        private static Synergy CreateSynergy(Enums.Synergy synergy, Enums.SynergyType type, Enums.SynergyBuffTarget target,
            int stage1, int stage2 = 0, int stage3 = 0)
        {
            Synergy newSynergy = new Synergy(synergy, type, target, stage1, stage2, stage3);
            Synergies[(int)synergy] = newSynergy;
            return newSynergy;
        }

        private static void BlessAllUnits(Enums.MatchState matchState)
        {
            if (matchState == Enums.MatchState.Blessing)
            {
                HashSet<Figure> allFigures = new HashSet<Figure>();
                foreach (List<Figure> figures in FiguresInSynergy)
                {
                    foreach (Figure figure in figures)
                    {
                        if (!allFigures.Contains(figure))
                            figure.ReceiveBlessings(GetBuffsForFigure(figure));
                        allFigures.Add(figure);
                    }
                }
            }
        }
    }
}
