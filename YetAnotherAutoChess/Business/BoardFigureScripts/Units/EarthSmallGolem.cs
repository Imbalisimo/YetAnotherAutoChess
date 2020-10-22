using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.BoardFigureScripts.Units
{
    class EarthSmallGolem : Unit
    {
        public EarthSmallGolem()
        {
            Cost = 1;
            Star = 1;
            Stats.Range = 1;
            Stats.Health = 900;
            CurrentHealth = Stats.Health;
            Stats.Armor = 15;
            Stats.AttackSpeed = 1.5f;
            Stats.MagicResist = 6;
            Stats.AttackDamage = 50;
            Stats.AutoAttackDamageType = Enums.DamageType.Physical;
            Stats.Mana = float.PositiveInfinity;
        }

        public override Attack Ability()
        {
            throw new NotImplementedException();
        }

        public override Attack AutoAttack()
        {
            Attack attack = new Attack(Enums.TargetingSystem.Target, Enums.DamageType.Physical, 0, Stats.Range, Stats.AttackDamage);
            return attack;
        }

        public override string GetAbilityDescription()
        {
            return "";
        }

        public override void MakeMeAOneStar()
        {
            throw new NotImplementedException();
        }

        public override void MakeMeAThreeStar()
        {
            throw new NotImplementedException();
        }

        public override void MakeMeATwoStar()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {

        }
    }
}
