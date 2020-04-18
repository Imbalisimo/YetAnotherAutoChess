using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.BoardFigureScripts.Spells
{
    public abstract class SpellAOE : Spell
    {
        protected Figure _center;
        protected int _range;
    }
}
