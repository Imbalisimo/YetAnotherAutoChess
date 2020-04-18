using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YetAnotherAutoChess.Business.GameAssets;

namespace YetAnotherAutoChess.Business
{
    public class Figure : GameObject
    {
        public struct Place
        {
            public int Row;
            public int Column;
        }

        public List<Figure> Sacrifices;

        public Place Position;
        public bool Untargetable;

        public ShieldPriorityQueue shieldPriorityQueue = new ShieldPriorityQueue();
        public Unit Unit;
        public Piece Piece;
        //public FigureUIManager FigureUIManager;
        public int Cost;

        public Enums.Synergy Mythology { get => Unit.Stats.Synergies[0]; }
        public Enums.Synergy Diety { get => Unit.Stats.Synergies[1]; }
        public Enums.Synergy AdditionalDiety { get => Unit.Stats.Synergies.Count > 2 ? Unit.Stats.Synergies[2] : Enums.Synergy.none; }

        public int Star { get => Unit.Star; }

        public string Owner { get; set; }

        public int Range { get { return Unit.Stats.Range; } }
        public float CurrentHealth { get { return Unit.CurrentHealth; } }

        private Figure _target;
        public Figure Target { get => _target; }

        public void CarryEnemyColors()
        {
           // FigureUIManager.CarryEnemyColors();
        }

        public delegate void Sell(Figure figure);
        public event Sell OnSell;
        public Figure()
        {
            Sacrifices = new List<Figure>();
            //FigureUIManager.OnFigureSellClick += () => OnSell(this.gameObject);
            //FigureUIManager.OnPieceToggleClick += toggle => Piece.Toggle();
            //FigureUIManager.SetPieceToggleText(Piece.PieceType);
            //FigureUIManager.SetSpellTooltip(Unit.GetAbilityDescription());
            //FigureUIManager.SetSpellImage(Unit);
        }

        public override void Update()
        {
            AttackOrMove();
            UpdateHealthAndMana();
        }

        public void UpdateHealthAndMana()
        {
            //FigureUIManager.SetHealth(Unit.CurrentHealth / Unit.Stats.Health);
            //FigureUIManager.SetMana(Unit.CurrentMana / Unit.Stats.Mana);
        }

        private bool _startOfMatch = false;
        private DateTime _lastAttacked = new DateTime();
        public void AttackOrMove()
        {
            if (MatchManager.Instance.MatchState != Enums.MatchState.Battle)
                return;

            if (Untargetable)
                return;

            if (Piece.PieceType == Enums.Piece.Knight && _startOfMatch)
            {
                _startOfMatch = false;
                Point destination = Dijkstra.KnightJumpOnStart(this);
                MoveTo((int)destination.X, (int)destination.Y);
                return;
            }

            if (Time.TimeDifferenceFromNowInSeconds(_lastAttacked) < Unit.Stats.AttackSpeed)
                return;

            if (Unit.Stats.Stunned > 0)
                return;

            _lastAttacked = DateTime.Now;

            if (Unit.CurrentMana >= Unit.Stats.Mana && Unit.Stats.Silenced <= 0)
            {
                CastAbility();
                return;
            }
            if (_target != null && !_target.Untargetable && Dijkstra.IsEnemyInRange(this, _target))
            {
                if (Unit.Stats.Disarmed <= 0)
                {
                    Attack();
                    return;
                }
            }
            else
            {
                TakeAStep(Range);
            }
        }

        private void CastAbility()
        {
            //        if (Dijkstra.EnemyInsideRange(this, Unit.Stats.AbilityRange) == null)
            {
                if (TakeAStep(Unit.Stats.AbilityRange) == false) return;
                //                  ApproachAnEnemy();
            }
            Attack attack = Unit.Ability();
            attack.AddSource(this);
            this.Unit.CurrentMana = 0;
        }

        private void Attack()
        {
            int maxManaGainPerAttack = 10;
            Unit.CurrentMana += maxManaGainPerAttack;
            Attack attack = Unit.AutoAttack();
            attack.AddSource(this);
        }

        private bool TakeAStep(int range)
        {
            Figure nextTarget = Dijkstra.EnemyInsideRange(this, range);
            if (nextTarget == null)
            {
                ApproachAnEnemy();
                return false;
            }
            else
            {
                _target = nextTarget;
                return true;
            }
        }

        private void ApproachAnEnemy()
        {
            Point nextPosition;
            if (Piece.PieceType != Enums.Piece.Knight)
                nextPosition = Dijkstra.FindNextStep(this);
            else
                nextPosition = Dijkstra.FindNextStep(this, true);
            if (nextPosition.X < 0 || nextPosition.Y < 0)
                return;
            MoveTo(nextPosition.X, nextPosition.Y);
        }

        private void MoveTo(double row, double column)
        {
            int r = (int)row;
            int c = (int)column;
            OnMove(this, r, c);
            Position.Row = r;
            Position.Column = c;
        }

        public delegate void Move(Figure sender, int nextRow, int nextColumn);
        public event Move OnMove;

        public void ReceiveBlessings(List<Buff> blessings)
        {
            foreach (Buff buff in blessings)
                AddBuff(buff);
        }

        public float TakeDamage(Enums.DamageType damageType, float damage, out float damageReturn, List<Buff> buffs = null)
        {
            damageReturn = 0;
            float damageDealt = TakeDamage(damageType, damage, buffs);
            if (Unit.Stats.HasDamageReturn > 0)
                damageReturn = damage - damageDealt;

            return damageDealt;
        }

        public float TakeDamage(Enums.DamageType damageType, float damage, List<Buff> buffs = null)
        {
            float damageDealt;

            if (Untargetable || Unit.Stats.IsInvounrable > 0)
                return 0;

            damageDealt = CalculateDamage(damageType, damage);

            float damageExceedsShield = Unit.Stats.Shield -= damage;
            if (damageExceedsShield > 0)
                Unit.CurrentHealth -= damageExceedsShield;
            else
            {
                Unit.CurrentHealth -= damageDealt;
            }

            if (Unit.CurrentHealth <= 0)
                Die();
            Unit.CurrentMana += Math.Min(50, (damage / Unit.Stats.Health) * Unit.Stats.Mana);

            if (buffs != null)
                foreach (Buff buff in buffs)
                    AddBuff(buff);

            return damageDealt;
        }

        private float CalculateDamage(Enums.DamageType damageType, float damage)
        {
            float damageDealt;
            switch (damageType)
            {
                case Enums.DamageType.Magical:
                    damageDealt = damage * 30 / (30 + Unit.Stats.MagicResist);
                    break;
                case Enums.DamageType.Physical:
                    damageDealt = damage * 30 / (30 + Unit.Stats.Armor);
                    break;
                default:
                    damageDealt = damage;
                    break;
            }
            damageDealt -= Unit.Stats.DamageReduction;
            damageDealt *= (100 - Unit.Stats.DamageReductionPercentage) / 100;
            return damageDealt;
        }

        private void AddBuff(Buff buff)
        {
            // Possible errors if buff is cast and not recognized as ordinary buff afterwards
            if (buff is BoardFigureScripts.BuffExtensions.ISourcable)
                (buff as BoardFigureScripts.BuffExtensions.ISourcable).AddSource(this);
            if (buff is BoardFigureScripts.BuffExtensions.IStackable)
            {
                Buff alreadyAppliedBuff = Unit.Buffs.Find(o => o.GetType() == buff.GetType());
                if (alreadyAppliedBuff != null)
                {
                    float damage;
                    Enums.DamageType damageType;
                    (damage, damageType) = (alreadyAppliedBuff as BoardFigureScripts.BuffExtensions.IStackable).AddStack();
                    TakeDamage(damageType, damage);
                    buff.Destroy();
                    return;
                }
            }

            //switch(buff)
            //{
            //    //case SynergyBuffs.BuffFromSource b:
            //    //    b.AddSource(this);
            //    //    break;
            //}

            if (buff.BuffStats.ShieldFlat != 0 || buff.BuffStats.ShieldPercentage != 0)
            {
                shieldPriorityQueue.Enqueue(buff, Unit.Stats);
            }

            //if(Unit.Buffs!=null)
            Unit.Buffs.Add(buff);

            buff.ApplyToProperties(Unit.Stats);
            buff.OnExpire += o => o.RemoveFromProperties(Unit.Stats, shieldPriorityQueue);

            buff.Commence();
            //buff.OnExpire += o => Unit.Stats -= o.BuffStats;
        }

        public int DPS { get; set; }

        public void DamageFeedback(Enums.DamageType damageType, float damage)
        {
            DPS += (int)damage;
            switch (damageType)
            {
                case Enums.DamageType.Physical:
                    if (Unit.Stats.LifeSteal > 0)
                        Heal(damage * Unit.Stats.LifeSteal / 100);
                    break;
                case Enums.DamageType.Magical:
                    if (Unit.Stats.SpellWamp > 0)
                        Heal(damage * Unit.Stats.SpellWamp / 100);
                    break;
                default:
                    break;
            }
        }

        public void Heal(float health, List<Buff> buffs = null)
        {
            Unit.CurrentHealth += health;
            if (Unit.CurrentHealth > Unit.Stats.Health)
                Unit.CurrentHealth = Unit.Stats.Health;
        }

        public delegate void Death(Figure sender);
        public event Death OnDeath;
        public void Die()
        {
            MainViewModel.SetActive(false);
            Untargetable = true;
            OnDeath(this);
        }

        private Place _matchStartingPosition;
        public Place MatchStartingPosition { get => _matchStartingPosition; }
        public void Restart()
        {
            MainViewModel.SetActive(true);
            Untargetable = true;
            Unit.Buffs.ForEach(b => b.Dispell());
            Unit.Buffs.Clear();
            MoveTo(_matchStartingPosition.Row, _matchStartingPosition.Column);
            Unit.CurrentHealth = Unit.Stats.Health;
            Unit.CurrentMana = Unit.Stats.StartingMana;
            //FigureUIManager.EnableToggle();
        }

        public void PrepareForBattle()
        {
            Untargetable = false;
            _matchStartingPosition.Row = Position.Row;
            _matchStartingPosition.Column = Position.Column;
            Buff pieceBuff = Piece.GrantBuff();
            if (pieceBuff != null)
            {
                AddBuff(pieceBuff);
            }

            Unit.CurrentHealth = Unit.Stats.Health;
            Unit.CurrentMana = Unit.Stats.StartingMana;
            //FigureUIManager.ToggleDisable();
            _startOfMatch = true;
            DPS = 0;
        }

        public void Upgrade(Figure figure1, Figure figure2)
        {
            Sacrifices.Add(figure1);
            Sacrifices.Add(figure2);
            if (Star == 2)
            {
                Unit.MakeMeAThreeStar();
                //FigureUIManager.PromoteToThreeStar();
            }
            if (Star == 1)
            {
                Unit.MakeMeATwoStar();
                //FigureUIManager.PromoteToTwoStar();
            }
        }
    }
}
