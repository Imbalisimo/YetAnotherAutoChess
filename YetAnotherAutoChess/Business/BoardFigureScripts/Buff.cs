using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business
{
    public abstract class Buff : GameObject
    {
        public Buff(Buff.BuffProperties buffStats, float duration) : this()
        {
            _buffStats = buffStats;
            _duration = duration;
        }

        public Buff()
        {
            _buffStats.AttackCarriedBuffs = new List<Buff>();
            _buffStats.SpellCarriedBuffs = new List<Buff>();
        }

        //public virtual void Initialize()
        //{
        //    Initialize(_buffStats, _duration);  // When overriding, call this function
        //    _buffStats.AttackCarriedBuffs = new List<Buff>();
        //    _buffStats.SpellCarriedBuffs = new List<Buff>();
        //}
        //public void Initialize(Buff.BuffProperties buffStats, float duration)
        //{
        //    _buffStats = buffStats;
        //    _duration = duration;
        //}

        private Buff.BuffProperties _buffStats;
        public Buff.BuffProperties BuffStats { get => _buffStats; }

        private float _duration;
        private DateTime _startTime;

        public void Commence()
        {
            _startTime = DateTime.Now;
        }

        public delegate void Expire(Buff sender);
        public event Expire OnExpire;

        // Update is called once per frame
        public override void Update()
        {
            if (Time.TimeDifferenceFromNowInSeconds(_startTime) > _duration)
            {
                Dispell();
            }
        }

        public void Dispell()
        {
            OnExpire(this);
            Destroy();
        }

        public virtual float ApplyToProperties(Unit.Properties property)
        {
            property.Range += _buffStats.RangeFlat;
            property.Range *= (100 + _buffStats.RangePercentage) / 100;

            property.AbilityRange += _buffStats.RangeFlat;                         // Range also buffs ability range
            property.AbilityRange *= (100 + _buffStats.RangePercentage) / 100;

            property.AttackSpeed += _buffStats.AttackSpeedFlat;
            property.AttackSpeed *= (100 + _buffStats.AttackSpeedPercentage) / 100;      // Attack speed adds to itself in percentage

            property.AttackDamage += _buffStats.AttackDamageFlat;
            property.AttackDamage *= (100 + _buffStats.AttackDamagePercentage) / 100;

            property.AbilityPower += _buffStats.AbilityPowerFlat;
            property.AbilityPower *= (100 + _buffStats.AbilityPowerPercentage) / 100;

            if (_buffStats.AutoAttackDamageType != Enums.DamageType.none)
                property.AutoAttackDamageType = _buffStats.AutoAttackDamageType;
            if (_buffStats.AbilityDamageType != Enums.DamageType.none)
                property.AbilityDamageType = _buffStats.AbilityDamageType;

            property.Health += _buffStats.HealthFlat;
            //if (_buffStats.HealthFlat > 0)
            //    property.OnMaxHpIncrease(_buffStats.Health);

            property.Mana += _buffStats.ManaFlat;
            property.Mana *= (100 + _buffStats.ManaPercentage) / 100;                    // Mana adds to itself in percentage

            property.StartingMana += _buffStats.StartingMana;

            property.Armor += _buffStats.ArmorFlat;
            property.Armor *= (100 + _buffStats.ArmorPercentage) / 100;

            property.MagicResist += _buffStats.MagicResistFlat;
            property.MagicResist *= (100 + _buffStats.MagicResistPercentage) / 100;

            property.Shield += _buffStats.ShieldFlat;
            property.Shield *= (100 + _buffStats.ShieldPercentage) / 100;

            property.DamageReduction += _buffStats.DamageReduction;
            property.DamageReductionPercentage += _buffStats.DamageReductionPercentage;

            property.LifeSteal += _buffStats.LifeSteal;
            property.SpellWamp += _buffStats.SpellWamp;

            property.Stunned += _buffStats.Stunned;
            property.Silenced += _buffStats.Silenced;
            property.Disarmed += _buffStats.Disarmed;

            property.IsInvounrable += _buffStats.IsInvounrable;
            property.HasDamageReturn += _buffStats.HasDamageReturn;

            if (property.AttackCarriedBuffs == null)
                property.AttackCarriedBuffs = new List<Buff>();
            if (_buffStats.AttackCarriedBuffs != null)
                property.AttackCarriedBuffs.AddRange(_buffStats.AttackCarriedBuffs);

            if (property.SpellCarriedBuffs == null)
                property.SpellCarriedBuffs = new List<Buff>();
            if (_buffStats.SpellCarriedBuffs != null)
                property.SpellCarriedBuffs.AddRange(_buffStats.SpellCarriedBuffs);

            return _buffStats.HealthFlat;
        }
        internal void RemoveFromProperties(Unit.Properties property, ShieldPriorityQueue shieldPriorityQueue)
        {

            property.Range -= _buffStats.RangeFlat;
            property.Range /= 1 + _buffStats.RangePercentage;

            property.AbilityRange -= _buffStats.RangeFlat;                       // Range also buffs ability range
            property.AbilityRange /= 1 + _buffStats.RangePercentage;

            property.AttackSpeed -= _buffStats.AttackSpeedFlat;
            property.AttackSpeed /= 1 + _buffStats.AttackSpeedPercentage;              // Attack speed adds to itself in percentage

            property.AttackDamage -= _buffStats.AttackDamageFlat;
            property.AttackDamage /= 1 + _buffStats.AttackDamagePercentage;

            property.AbilityPower -= _buffStats.AbilityPowerFlat;
            property.AbilityPower /= 1 + _buffStats.AbilityPowerPercentage;

            property.Health -= _buffStats.HealthFlat;
            property.Health /= 1 + _buffStats.HealthPercentage;

            property.Mana -= _buffStats.ManaFlat;
            property.Mana /= 1 + _buffStats.ManaPercentage;                            // Mana adds to itself in percentage

            property.StartingMana -= _buffStats.StartingMana;

            property.Armor -= _buffStats.ArmorFlat;
            property.Armor /= 1 + _buffStats.ArmorPercentage;

            property.MagicResist -= _buffStats.MagicResistFlat;
            property.MagicResist /= 1 + _buffStats.MagicResistPercentage;

            if (_buffStats.ShieldFlat + _buffStats.ShieldPercentage < 0)
                property.Shield = shieldPriorityQueue.Dequeue(this, property.Shield);

            property.DamageReduction -= _buffStats.DamageReduction;
            property.DamageReductionPercentage -= _buffStats.DamageReductionPercentage;

            property.LifeSteal -= _buffStats.LifeSteal;
            property.SpellWamp -= _buffStats.SpellWamp;
            property.BuffEfficiencyCoefficient -= _buffStats.BuffEfficiencyCoefficient;

            property.IsInvounrable -= _buffStats.IsInvounrable;
            property.HasDamageReturn -= _buffStats.HasDamageReturn;

            foreach (Buff buff in _buffStats.AttackCarriedBuffs)
                property.AttackCarriedBuffs.Remove(buff);
            foreach (Buff buff in _buffStats.SpellCarriedBuffs)
                property.SpellCarriedBuffs.Remove(buff);
        }

        public struct BuffProperties
        {
            public int RangeFlat;
            public int RangePercentage;

            public float AttackSpeedFlat;
            public float AttackSpeedPercentage;


            public float AttackDamageFlat;
            public float AttackDamagePercentage;

            public float AbilityPowerFlat;
            public float AbilityPowerPercentage;

            public Enums.DamageType AutoAttackDamageType;
            public Enums.DamageType AbilityDamageType;

            public float HealthFlat;
            public float HealthPercentage;

            public float ManaFlat;
            public float ManaPercentage;

            public float StartingMana;

            public float ArmorFlat;
            public float ArmorPercentage;

            public float MagicResistFlat;
            public float MagicResistPercentage;

            public float ShieldFlat;
            public float ShieldPercentage;
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
            //buffs that apply to hit figures
            public List<Buff> AttackCarriedBuffs;
            public List<Buff> SpellCarriedBuffs;
        }
    }
}
