using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.BoardFigureScripts.Units
{
    public class EarthBigGolem : Unit
    {
        public EarthBigGolem()
        {
            Cost = 1;
            Star = 2;
            Stats.Range = 1;
            Stats.Health = 1400;
            CurrentHealth = Stats.Health;
            Stats.Armor = 25;
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
