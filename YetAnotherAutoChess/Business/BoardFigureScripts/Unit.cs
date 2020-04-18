using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business
{
    public abstract class Unit : GameObject
    {
        public Unit()
        {
            Stats = new Properties();
            Stats.OnMaxHpIncrease += hp => CurrentHealth += hp;
            Buffs = new List<Buff>();
        }

        public int Cost;
        public Properties Stats;
        public float CurrentHealth;
        public float CurrentMana;
        public int Star;

        internal abstract Attack AutoAttack();
        internal abstract Attack Ability();

        public abstract string GetAbilityDescription();

        public List<Buff> Buffs;

        public abstract void MakeMeAOneStar();
        public abstract void MakeMeATwoStar();
        public abstract void MakeMeAThreeStar();


        public class Properties
        {
            public int Range;
            public int AbilityRange;
            public float AttackSpeed;
            public float AttackDamage;
            public float AbilityPower;
            public Enums.DamageType AutoAttackDamageType;
            public Enums.DamageType AbilityDamageType;

            public float Health;
            public float Mana;
            public float StartingMana;
            public float Armor;
            public float MagicResist;
            public float Shield;
            //------------------------------------------
            public float DamageReduction;
            public float DamageReductionPercentage;

            public float LifeSteal;
            public float SpellWamp;
            public float BuffEfficiencyCoefficient;
            //negative status
            public byte Stunned;
            public byte Silenced;
            public byte Disarmed;
            //for special cases
            public byte IsInvounrable;
            public byte HasDamageReturn;

            public List<Enums.Synergy> Synergies;

            public List<Buff> AttackCarriedBuffs;
            public List<Buff> SpellCarriedBuffs;

            public delegate void MaxHpIncrease(float health);
            public event MaxHpIncrease OnMaxHpIncrease;
            public delegate void MaxManaIncrease(float health);
            public event MaxManaIncrease OnMaxManaIncrease;
        }
    }
}
