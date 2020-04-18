using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.BoardFigureScripts.Spells
{
    public abstract class SpellSingleTarget : Spell
    {
        private Enums.DamageType _damageType;
        private float _damage;

        public SpellSingleTarget(Enums.DamageType damageType, float damage)
        {
            _damageType = damageType;
            _damage = damage;
        }

        public override void Activate(Figure _target, Figure _source, List<Buff> spellCarriedBuffs)
        {
            _source.DamageFeedback(_damageType, _target.TakeDamage(_damageType, _damage, spellCarriedBuffs));
        }
    }
}
