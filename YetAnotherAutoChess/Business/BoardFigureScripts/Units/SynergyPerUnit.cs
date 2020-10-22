using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetAnotherAutoChess.Enums;

namespace YetAnotherAutoChess.Business.BoardFigureScripts.Units
{
    public static class SynergyPerUnit
    {
        public static Synergy GetSynergiesFor(Unit unit)
        {
            Synergy synergies;
            switch(unit)
            {
                case Thor t:
                    synergies = Synergy.Norse | Synergy.Thunder;
                    break;
                default:
                    return Synergy.none;
            }
            return synergies;
        }
    }
}
