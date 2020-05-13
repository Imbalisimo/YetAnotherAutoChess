using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetAnotherAutoChess.Enums;

namespace YetAnotherAutoChess.Business.BoardFigureScripts.Units
{
    class Thor : Unit
    {
        public Thor()
        {
            Cost = 1;
            Stats.Health = 550;
            CurrentHealth = Stats.Health;
            Stats.Mana = 150;
            //CurrentMana = Stats.Mana;
            Stats.AttackDamage = 50;
            Stats.Range = 1;
            Stats.AttackSpeed = 2;
            Stats.AbilityRange = 1;
            Star = 1;
            Stats.Synergies = new List<Enums.Synergy>();
            Stats.Synergies.Add(Enums.Synergy.Norse);   // Mythology
            Stats.Synergies.Add(Enums.Synergy.Thunder); // Diety
        }

        public override Attack Ability()
        {
            SpellMjolnirBlow spellMjolnirBlow = 
                new SpellMjolnirBlow(Stats.AbilityDamageType, (Stats.AbilityPower + 100) / 100 * Star * 200);
            Attack attack = new Attack(Enums.TargetingSystem.Target, Enums.DamageType.Magical, 0, Stats.AbilityRange, 0, spellMjolnirBlow);
            return attack;
        }

        public override Attack AutoAttack()
        {
            return new Attack(Enums.TargetingSystem.Target, Enums.DamageType.Physical, 0, Stats.Range, Stats.AttackDamage);
        }

        public override string GetAbilityDescription()
        {
            return "strike an enemy with thunder coated Mjolnir";
        }

        public override void MakeMeAOneStar()
        {
            Stats.Health = 550;
            Stats.Mana = 150;
            Stats.AttackDamage = 50;
            Star = 1;
        }

        public override void MakeMeATwoStar()
        {
            Stats.Health = 1000;
            Stats.Mana = 100;
            Stats.AttackDamage = 75;
            Star = 2;
        }

        public override void MakeMeAThreeStar()
        {
            Stats.Health = 1500;
            Stats.Mana = 75;
            Stats.AttackDamage = 100;
            Star = 3;
        }

        public override void Update()
        {
            
        }

        public class SpellMjolnirBlow : Spells.SpellSingleTarget
        {
            public SpellMjolnirBlow(DamageType damageType, float damage) : base(damageType, damage)
            {
            }
        }
    }
}
