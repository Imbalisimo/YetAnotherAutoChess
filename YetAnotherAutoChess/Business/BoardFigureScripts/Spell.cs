using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business
{
    public abstract class Spell : GameObject
    {
        protected Figure _target;
        protected Figure _source;

        public override void Update()
        {

        }

        public abstract void Activate(Figure target, Figure source, List<Buff> spellCarriedBuffs);
    }
}
