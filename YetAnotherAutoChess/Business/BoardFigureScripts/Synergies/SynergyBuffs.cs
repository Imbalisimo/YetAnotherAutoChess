using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetAnotherAutoChess.Business.GameAssets;

namespace YetAnotherAutoChess.Business.BoardFigureScripts.Synergies
{
    public static class SynergyBuffs
    {
        public static Buff CreateBlessing(Enums.Synergy synergy, int stage)
        {
            switch (synergy)
            {
                // MYTHOLOGY
                case Enums.Synergy.Aztec:
                    return CreateAztec(stage);
                case Enums.Synergy.Celtic:
                    return CreateCeltic(stage);
                case Enums.Synergy.Chinese:
                    return CreateChinese(stage);
                case Enums.Synergy.Egyptian:
                    return CreateEgyptian(stage);
                case Enums.Synergy.Greek:
                    return CreateGreek(stage);
                case Enums.Synergy.Hindu:
                    return CreateHindu(stage);
                case Enums.Synergy.Japanese:
                    return CreateJapanese(stage);
                case Enums.Synergy.Maya:
                    return CreateMaya(stage);
                case Enums.Synergy.Norse:
                    return CreateNorse(stage);
                case Enums.Synergy.Sumerian:
                    return CreateSumerian(stage);
                case Enums.Synergy.Voodoo:
                    return CreateVoodoo(stage);

                // DIETY
                case Enums.Synergy.Beauty:
                    return CreateBeauty(stage);
                case Enums.Synergy.Chief:
                    return CreateChief(stage);
                case Enums.Synergy.Dragon:
                    return CreateDragon(stage);
                case Enums.Synergy.Earth:
                    return CreateEarth(stage);
                case Enums.Synergy.Harvest:
                    return CreateHarvest(stage);
                case Enums.Synergy.Hunt:
                    return CreateHunt(stage);
                case Enums.Synergy.Thunder:
                    return CreateThunder(stage);
                case Enums.Synergy.Trickster:
                    return CreateTrickster(stage);
                case Enums.Synergy.Underworld:
                    return CreateUnderworld(stage);
                case Enums.Synergy.War:
                    return CreateWar(stage);
                case Enums.Synergy.Water:
                    return CreateWater(stage);
                case Enums.Synergy.Wisdom:
                    return CreateWisdom(stage);

                default:
                    return null;
            }
        }

        public abstract class BuffTransform : Buff
        {
            public BuffTransform(BuffProperties stats, float duration) : base(stats, duration)
            {

            }

            private Unit.Properties _unitProperties;
            public override float ApplyToProperties(Unit.Properties property)
            {
                _unitProperties = property;
                return 0;
            }

            public void Transform()
            {
                base.ApplyToProperties(_unitProperties);
            }
        }

        public delegate void FigureSpawn(Figure figure, Figure encirclement);

        public static void SpawnFigure(Figure figure, Figure encirclement)
        {
            Point p = Dijkstra.FindNextStep(encirclement);
            Board.ImpaleToPosition(encirclement, p.X, p.Y);
            figure.Position.Column = (int)p.X;
            figure.Position.Row = (int)p.Y;
        }

        #region Mythology
        #region Aztec
        // All damage is physical damage
        private static Buff CreateAztec(int stage)
        {
            switch (stage)
            {
                case 1:
                    Buff.BuffProperties buffProperties = new Buff.BuffProperties();
                    buffProperties.AbilityDamageType = Enums.DamageType.Physical;
                    buffProperties.AutoAttackDamageType = Enums.DamageType.Physical;

                    BuffAztec buff = new BuffAztec(buffProperties, float.PositiveInfinity);

                    return buff;

                default:
                    return null;
            }
        }

        public class BuffAztec : Buff
        {
            public BuffAztec(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }
        #endregion
        #region Celtic
        // Get attack speed on ult
        private static Buff CreateCeltic(int stage)
        {
            Buff.BuffProperties buffProperties = new Buff.BuffProperties();
            switch (stage)
            {
                case 1:
                    buffProperties.AttackSpeedPercentage = 50;
                    break;
                case 2:
                    buffProperties.AttackSpeedPercentage = 100;
                    break;
                case 3:
                    buffProperties.AttackSpeedPercentage = 150;
                    break;
                default:
                    return null;
            }
            BuffAztec buff = new BuffAztec(buffProperties, float.PositiveInfinity);

            return buff;
        }

        public class BuffCeltic : BuffTransform
        {
            public BuffCeltic(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }
        #endregion
        #region Chinese
        // Has damage return
        private static Buff CreateChinese(int stage)
        {
            switch (stage)
            {
                case 1:
                    Buff.BuffProperties buffProperties = new Buff.BuffProperties();
                    buffProperties.HasDamageReturn++;

                    BuffChinese buff = new BuffChinese(buffProperties, float.PositiveInfinity);

                    return buff;
                default:
                    return null;
            }
        }

        public class BuffChinese : Buff
        {
            public BuffChinese(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }
        #endregion
        #region Egyptian
        // Burn surrounding enemies 
        private static Buff CreateEgyptian(int stage)
        {
            float damage = 0;
            switch (stage)
            {
                case 1:
                    damage = 30;
                    break;
                case 2:
                    damage = 60;
                    break;
                case 3:
                    damage = 100;
                    break;
                default:
                    return null;
            }

            BuffEgyptian buff = new BuffEgyptian(damage, Enums.DamageType.Magical);

            return buff;
        }
        public class BuffEgyptian : BuffExtensions.BuffWithTimeTrigger, BuffExtensions.ISourcable
        {
            private Figure _source;
            private float _damage;
            private Enums.DamageType _damageType;

            public BuffEgyptian(float damage, Enums.DamageType damageType) : base(new Buff.BuffProperties(), float.PositiveInfinity)
            {
                _damage = damage;
                _damageType = damageType;
            }

            public void AddSource(Figure figure)
            {
                _source = figure;
            }

            public override void OnTrigger()
            {
                int range = 1;
                for (int row = _source.Position.Row - range; row <= _source.Position.Row + range; ++row)
                {
                    if (row < 0 || row > 7)
                        continue;

                    for (int column = _source.Position.Column - range; column <= _source.Position.Column + range; ++column)
                    {
                        if (column < 0 || column > 7)
                            continue;

                        Figure figure = Board.Figures[row, column];
                        if (figure != null)
                            if (figure.Owner != _source.Owner)
                                _source.DamageFeedback(_damageType, figure.TakeDamage(_damageType, _damage));
                    }
                }
            }
        }
        #endregion
        #region Greek
        // restore X HP every second
        private static Buff CreateGreek(int stage)
        {
            int HPPerSecond = 0;
            switch (stage)
            {
                case 1:
                    HPPerSecond = 20;
                    break;
                case 2:
                    HPPerSecond = 45;
                    break;
                case 3:
                    HPPerSecond = 100;
                    break;
                default:
                    return null;
            }

            BuffGreek buff = new BuffGreek(HPPerSecond);

            return buff;
        }
        public class BuffGreek : BuffExtensions.BuffWithTimeTrigger, BuffExtensions.ISourcable
        {
            private Figure _target;
            private int _hpPerSecond;

            public BuffGreek(int HpPerSecond) : base(new Buff.BuffProperties(), float.PositiveInfinity)
            {

            }

            public void AddSource(Figure figure)
            {
                _target = figure;
            }

            public override float ApplyToProperties(Unit.Properties property)
            {
                return 0;
            }

            public override void OnTrigger()
            {
                _target.Heal(_hpPerSecond * secondsBewteenTriggers);
            }
        }
        #endregion
        #region Hindu
        // Basic attacks deal AOE, prone to buggs, so requires extra testing
        private static Buff CreateHindu(int stage)
        {
            float damageCoefficient = 0;
            switch (stage)
            {
                case 1:
                    damageCoefficient = 0.5f;
                    break;
                case 2:
                    damageCoefficient = 1f;
                    break;
                case 3:
                    damageCoefficient = 1.5f;
                    break;
                default:
                    return null;
            }

            BuffHinduExplosion buffE = new BuffHinduExplosion(damageCoefficient);

            Buff.BuffProperties buffProperties = new Buff.BuffProperties();
            buffProperties.AttackCarriedBuffs.Add(buffE);

            BuffHindu buff = new BuffHindu(buffProperties, float.PositiveInfinity);

            return buff;
        }

        public class BuffHindu : Buff, BuffExtensions.ISourcable
        {
            private Figure _source;

            public BuffHindu(BuffProperties stats, float duration) : base(stats, duration)
            {

            }

            public void AddSource(Figure figure)
            {
                _source = figure;
            }

            public override float ApplyToProperties(Unit.Properties property)
            {
                base.ApplyToProperties(property);
                BuffHinduExplosion buff = property.AttackCarriedBuffs.Find(b => { return b is BuffHinduExplosion; }) as BuffHinduExplosion;
                buff.AddOrigin(_source);
                return 0;
            }
        }

        public class BuffHinduExplosion : Buff, BuffExtensions.ISourcable
        {
            private Figure _source;
            private Figure _target;
            private float _damageCoefficient;

            public BuffHinduExplosion(float damageConstant) : base(new BuffProperties(), float.PositiveInfinity)
            {
                _damageCoefficient = damageConstant;
            }

            public void AddSource(Figure figure)
            {
                _target = figure;
            }

            public void AddOrigin(Figure figure)
            {
                _source = figure;
            }

            public override float ApplyToProperties(Unit.Properties property)
            {
                int range = 1;
                for (int row = _target.Position.Row - range; row <= _target.Position.Row + range; ++row)
                {
                    if (row < 0 || row > 7)
                        continue;

                    for (int column = _target.Position.Column - range; column <= _target.Position.Column + range; ++column)
                    {
                        if (column < 0 || column > 7)
                            continue;

                        if (column == _target.Position.Column && row == _target.Position.Row)
                            continue;

                        Figure figure = Board.Figures[row, column];
                        if (figure != null)
                            if (figure.Owner != _source.Owner)
                                _source.DamageFeedback(
                                    _source.Unit.Stats.AutoAttackDamageType,
                                    figure.TakeDamage(_source.Unit.Stats.AutoAttackDamageType,
                                        _source.Unit.Stats.AttackDamage * _damageCoefficient));
                    }
                }

                Destroy();
                return 0;
            }
        }
        #endregion
        #region Japanese
        // Grants shield
        private static Buff CreateJapanese(int stage)
        {
            bool randomDistribution;
            switch (stage)
            {
                case 1:
                    randomDistribution = true;
                    break;
                case 2:
                    randomDistribution = false;
                    break;
                default:
                    return null;
            }

            Buff.BuffProperties buffProperties = new Buff.BuffProperties();
            buffProperties.ShieldFlat = 500;

            BuffJapanese buff = new BuffJapanese(buffProperties,
                                                float.PositiveInfinity,
                                                randomDistribution,
                                                SynergyManager.FiguresInSynergy[(int)Enums.Synergy.Japanese].Count);

            return buff;
        }

        public class BuffJapanese : Buff
        {
            private bool _randomDistribution;
            private static int _synergyCarriers;

            public BuffJapanese(BuffProperties stats, float duration, bool randomDistribution, int carrierNumber) : base(stats, duration)
            {
                _randomDistribution = randomDistribution;
                _synergyCarriers = carrierNumber;
            }

            public override float ApplyToProperties(Unit.Properties property)
            {
                if (_randomDistribution)
                {
                    if (_synergyCarriers != (new Random()).Next(1, _synergyCarriers))
                    {
                        --_synergyCarriers;
                        return 0;
                    }
                }
                return base.ApplyToProperties(property);
            }
        }
        #endregion
        #region Maya
        // Basic attacks apply True damage DOT, prone to buggs
        private static Buff CreateMaya(int stage)
        {
            int damagePerSec = 0;
            switch (stage)
            {
                case 1:
                    damagePerSec = 50;
                    break;
                case 2:
                    damagePerSec = 150;
                    break;
                case 3:
                    damagePerSec = 250;
                    break;
                default:
                    return null;
            }

            BuffMayaOnHit buffE = new BuffMayaOnHit(damagePerSec);

            Buff.BuffProperties buffProperties = new Buff.BuffProperties();
            buffProperties.AttackCarriedBuffs.Add(buffE);

            BuffMaya buff = new BuffMaya(buffProperties, float.PositiveInfinity);

            return buff;
        }

        public class BuffMaya : Buff, BuffExtensions.ISourcable
        {
            public BuffMaya(BuffProperties stats, float duration) : base(stats, duration)
            {

            }

            private Figure _source;
            public void AddSource(Figure figure)
            {
                _source = figure;
            }

            public override float ApplyToProperties(Unit.Properties property)
            {
                base.ApplyToProperties(property);
                BuffHinduExplosion buff = property.AttackCarriedBuffs.Find(b => { return b is BuffHinduExplosion; }) as BuffHinduExplosion;
                buff.AddOrigin(_source);
                return 0;
            }
        }

        public class BuffMayaOnHit : BuffExtensions.BuffWithTimeTrigger, BuffExtensions.ISourcable
        {
            private Figure _source;
            private Figure _target;
            private float _damagePerSec;

            public BuffMayaOnHit(int damage) : base(new Buff.BuffProperties(), float.PositiveInfinity)
            {
                _damagePerSec = damage;
            }

            public void AddSource(Figure figure)
            {
                _target = figure;
            }

            public void AddOrigin(Figure figure)
            {
                _source = figure;
            }

            public override void OnTrigger()
            {
                _source.DamageFeedback(Enums.DamageType.True,
                    _target.TakeDamage(Enums.DamageType.True, _damagePerSec / secondsBewteenTriggers));
            }

            public override float ApplyToProperties(Unit.Properties property)
            {
                return 0;
            }
        }
        #endregion
        #region Norse
        // All damage is magical damage
        private static Buff CreateNorse(int stage)
        {
            switch (stage)
            {
                case 1:
                    Buff.BuffProperties buffProperties = new Buff.BuffProperties();
                    buffProperties.AbilityDamageType = Enums.DamageType.Magical;
                    buffProperties.AutoAttackDamageType = Enums.DamageType.Magical;

                    BuffNorse buff = new BuffNorse(buffProperties, float.PositiveInfinity);

                    return buff;

                default:
                    return null;
            }
        }

        public class BuffNorse : Buff
        {
            public BuffNorse(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }
        #endregion
        #region Sumerian
        // Ignore damage flat
        private static Buff CreateSumerian(int stage)
        {
            int damageReduction = 0;
            switch (stage)
            {
                case 1:
                    damageReduction = 25;
                    break;
                case 2:
                    damageReduction = 60;
                    break;
                case 3:
                    damageReduction = 100;
                    break;
                default:
                    return null;
            }

            Buff.BuffProperties buffProperties = new Buff.BuffProperties();
            buffProperties.DamageReduction = damageReduction;

            BuffSumerian buff = new BuffSumerian(buffProperties, float.PositiveInfinity);

            return buff;
        }

        public class BuffSumerian : Buff
        {
            public BuffSumerian(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }
        #endregion
        #region Voodoo
        // All damage is true damage
        private static Buff CreateVoodoo(int stage)
        {
            switch (stage)
            {
                case 1:
                    Buff.BuffProperties buffProperties = new Buff.BuffProperties();
                    buffProperties.AbilityDamageType = Enums.DamageType.True;
                    buffProperties.AutoAttackDamageType = Enums.DamageType.True;

                    BuffVoodoo buff = new BuffVoodoo(buffProperties, float.PositiveInfinity);

                    return buff;

                default:
                    return null;
            }
        }

        public class BuffVoodoo : Buff
        {
            public BuffVoodoo(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }
        #endregion
        #endregion

        #region Diety
        #region Beauty
        // Buffs are stronger
        private static Buff CreateBeauty(int stage)
        {
            Buff.BuffProperties buffProperties = new Buff.BuffProperties();
            switch (stage)
            {
                case 1:
                    buffProperties.BuffEfficiencyCoefficient = 1.5f;
                    break;
                case 2:
                    buffProperties.BuffEfficiencyCoefficient = 2.5f;
                    break;
                case 3:
                    buffProperties.BuffEfficiencyCoefficient = 4f;
                    break;
                default:
                    return null;
            }

            BuffBeauty buff = new BuffBeauty(buffProperties, float.PositiveInfinity);

            return buff;
        }

        public class BuffBeauty : Buff
        {
            public BuffBeauty(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }
        #endregion
        #region Chief
        // Grants health
        private static Buff CreateChief(int stage)
        {
            Buff.BuffProperties buffProperties = new Buff.BuffProperties();
            switch (stage)
            {
                case 1:
                    buffProperties.HealthFlat = 200;
                    break;
                case 2:
                    buffProperties.HealthFlat = 450;
                    break;
                case 3:
                    buffProperties.HealthFlat = 1000;
                    break;
                default:
                    return null;
            }

            BuffChief buff = new BuffChief(buffProperties, float.PositiveInfinity);

            return buff;
        }

        public class BuffChief : Buff
        {
            public BuffChief(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }
        #endregion
        #region Dragon
        // doubles ability power and attack damage
        private static Buff CreateDragon(int stage)
        {
            Buff.BuffProperties buffProperties = new Buff.BuffProperties();
            switch (stage)
            {
                case 1:
                case 3: //Every time all units get buff(Only if there is 1 unit or all units)
                    buffProperties.AttackDamagePercentage = 100;
                    buffProperties.AbilityPowerPercentage = 100;
                    break;
                case 2:
                default:
                    return null;
            }

            BuffDragon buff = new BuffDragon(buffProperties, float.PositiveInfinity);

            return buff;
        }

        public class BuffDragon : Buff
        {
            public BuffDragon(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }
        #endregion
        #region Earth
        // Spawn golem adds
        private static Buff CreateEarth(int stage)
        {
            GolemSize golemSize = GolemSize.none;
            switch (stage)
            {
                case 1:
                    golemSize = GolemSize.Small;
                    break;
                case 2:
                    golemSize = GolemSize.Big;
                    break;
                default:
                    return null;
            }

            BuffEarth buff = new BuffEarth(golemSize);
            buff.OnFigureSpawn += SpawnFigure;

            return buff;
        }

        public class BuffEarth : Buff, BuffExtensions.ISourcable
        {
            private static bool _triggered = false;
            private GolemSize _golemSize;
            private Figure _figure;
            private Figure _source;

            public event FigureSpawn OnFigureSpawn;

            public BuffEarth(GolemSize golemSize) : base(new Buff.BuffProperties(), float.PositiveInfinity)
            {
                _golemSize = golemSize;
                _triggered = false;
            }

            public void AddSource(Figure figure)
            {
                _source = figure;
            }

            public override float ApplyToProperties(Unit.Properties property)
            {
                Unit unit;
                if (!_triggered)
                {
                    switch (_golemSize)
                    {
                        case GolemSize.Small:
                            unit = FigureManager.CreateUnit("EarthSmallGolem");
                            break;
                        case GolemSize.Big:
                            unit = FigureManager.CreateUnit("EarthBigGolem");
                            break;

                        default:
                            return 0;
                    }

                    _figure = FigureManager.CreateFigure(unit, Enums.Piece.Pawn);
                    _triggered = true;

                    OnFigureSpawn(_figure, _source);
                }
                return 0;
            }

            public new void Dispell()
            {
                _figure.Destroy();
                base.Dispell();
            }
        }

        public enum GolemSize
        {
            Big,
            Small,
            none
        }
        #endregion
        #region Harvest
        // Invulnerable at the start of the combat
        private static Buff CreateHarvest(int stage)
        {
            float duration = 0;
            switch (stage)
            {
                case 1:
                    duration = 2;
                    break;
                case 2:
                    duration = 4;
                    break;
                case 3:
                    duration = 6;
                    break;
                default:
                    return null;
            }

            Buff.BuffProperties buffProperties = new Buff.BuffProperties();
            ++buffProperties.IsInvounrable;

            BuffHarvest buff = new BuffHarvest(buffProperties, duration);

            return buff;
        }

        public class BuffHarvest : Buff
        {
            public BuffHarvest(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }
        #endregion
        #region Hunt
        // Grants attack damage
        private static Buff CreateHunt(int stage)
        {
            Buff.BuffProperties buffProperties = new Buff.BuffProperties();
            switch (stage)
            {
                case 1:
                    buffProperties.AttackDamageFlat = 15;
                    break;
                case 2:
                    buffProperties.AttackDamageFlat = 40;
                    break;
                default:
                    return null;
            }

            BuffHunt buff = new BuffHunt(buffProperties, float.PositiveInfinity);

            return buff;
        }

        public class BuffHunt : Buff
        {
            public BuffHunt(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }
        #endregion
        #region Thunder
        // Stuns every X attacks
        private static Buff CreateThunder(int stage)
        {
            int stackNumber = 0;
            switch (stage)
            {
                case 1:
                    stackNumber = 5;
                    break;
                case 2:
                    stackNumber = 4;
                    break;
                case 3:
                    stackNumber = 3;
                    break;
                default:
                    return null;
            }

            BuffThunderOnHit buffE = new BuffThunderOnHit(stackNumber);

            Buff.BuffProperties buffProperties = new Buff.BuffProperties();
            buffProperties.AttackCarriedBuffs.Add(buffE);

            BuffThunder buff = new BuffThunder(buffProperties, float.PositiveInfinity);

            return buff;
        }

        public class BuffThunder : Buff
        {
            public BuffThunder(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }

        public class BuffThunderOnHit : Buff, BuffExtensions.ISourcable, BuffExtensions.IStackable
        {
            private int _stackNumber = 0;
            private Figure _target;
            private int _maxStacks;

            public BuffThunderOnHit(int maxStacks) : base(new Buff.BuffProperties(), float.PositiveInfinity)
            {
                SetMaxStacks(maxStacks);
            }

            public void AddSource(Figure figure)
            {
                _target = figure;
            }

            public void SetMaxStacks(int stacks, int damage = 0, Enums.DamageType damageType = Enums.DamageType.none)
            {
                _maxStacks = stacks;
            }

            public (float, Enums.DamageType) AddStack()
            {
                if (++_stackNumber >= _maxStacks)
                {
                    Buff.BuffProperties buffProperties = new Buff.BuffProperties();
                    ++buffProperties.Stunned;

                    BuffThunderProc buff = new BuffThunderProc(buffProperties, 1.5f);

                    List<Buff> buffs = new List<Buff>();
                    buffs.Add(buff);
                    Attack attack = new Attack(Enums.TargetingSystem.Self, Enums.DamageType.none, 0, 0, 0, null, buffs);
                    attack.AddSource(_target);
                }
                return (0, Enums.DamageType.none);
            }
        }

        public class BuffThunderProc : Buff
        {
            public BuffThunderProc(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }
        #endregion
        #region Trickster
        // Makes a silenced clone of himself
        // Tricksters must implement ICloneable
        private static Buff CreateTrickster(int stage)
        {
            switch (stage)
            {
                case 1:
                case 2:
                case 3:
                    break;
                default:
                    return null;
            }

            BuffTrickster buff = new BuffTrickster();
            buff.OnFigureSpawn += SpawnFigure;

            return buff;
        }

        public class BuffTrickster : Buff, BuffExtensions.ISourcable
        {
            private Figure _source;

            public event FigureSpawn OnFigureSpawn;

            public void AddSource(Figure figure)
            {
                _source = figure;
            }

            public override float ApplyToProperties(Unit.Properties property)
            {
                Unit unit = ((Units.ICloneable)_source.Unit).Clone();

                Figure figure = FigureManager.CreateFigure(unit, Enums.Piece.Pawn);

                OnFigureSpawn(figure, _source);

                return 0;
            }
        }
        #endregion
        #region Underworld
        // Ressurects 1st X figures to die (stops them from dying)
        private static Buff CreateUnderworld(int stage)
        {
            int reviveCount = 0;
            switch (stage)
            {
                case 1:
                    reviveCount = 2;
                    break;
                case 2:
                    reviveCount = 4;
                    break;
                default:
                    return null;
            }

            BuffUnderworld buff = new BuffUnderworld(reviveCount);

            return buff;
        }

        public class BuffUnderworld : Buff, BuffExtensions.IDeathInterferable, BuffExtensions.ISourcable
        {
            private static int _interferesRemaining;
            private Figure _source;

            public BuffUnderworld(int reviveCount) : base(new Buff.BuffProperties(), float.PositiveInfinity)
            {
                _interferesRemaining = reviveCount;
            }

            public void AddSource(Figure figure)
            {
                _source = figure;
            }

            public bool Interfere()
            {
                if (--_interferesRemaining < 0)
                    return false;

                _source.Heal(_source.Unit.Stats.Health);

                return true;
            }

            public override float ApplyToProperties(Unit.Properties property)
            {
                return 0;
            }
        }
        #endregion
        #region War
        // Grants armor
        private static Buff CreateWar(int stage)
        {
            Buff.BuffProperties buffProperties = new Buff.BuffProperties();
            switch (stage)
            {
                case 1:
                    buffProperties.ArmorFlat = 15;
                    break;
                case 2:
                    buffProperties.ArmorFlat = 30;
                    break;
                case 3:
                    buffProperties.ArmorFlat = 60;
                    break;
                default:
                    return null;
            }

            BuffWar buff = new BuffWar(buffProperties, float.PositiveInfinity);

            return buff;
        }

        public class BuffWar : Buff
        {
            public BuffWar(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }
        #endregion
        #region Water
        // Grants ability power
        private static Buff CreateWater(int stage)
        {
            Buff.BuffProperties buffProperties = new Buff.BuffProperties();
            switch (stage)
            {
                case 1:
                    buffProperties.AbilityPowerFlat = 50;
                    break;
                case 2:
                    buffProperties.AbilityPowerFlat = 100;
                    break;
                case 3:
                    buffProperties.AbilityPowerFlat = 150;
                    break;
                default:
                    return null;
            }

            BuffWater buff = new BuffWater(buffProperties, float.PositiveInfinity);

            return buff;
        }

        public class BuffWater : Buff
        {
            public BuffWater(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }
        #endregion
        #region Wisdom
        // Grants magic resist
        private static Buff CreateWisdom(int stage)
        {
            Buff.BuffProperties buffProperties = new Buff.BuffProperties();
            switch (stage)
            {
                case 1:
                    buffProperties.MagicResistPercentage = 40;
                    break;
                case 2:
                    buffProperties.MagicResistPercentage = 70;
                    break;
                case 3:
                    buffProperties.MagicResistPercentage = 150;
                    break;
                default:
                    return null;
            }

            BuffWisdom buff = new BuffWisdom(buffProperties, float.PositiveInfinity);

            return buff;
        }

        public class BuffWisdom : Buff
        {
            public BuffWisdom(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }
        #endregion
        #endregion
    }
}
