using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using YetAnotherAutoChess.Enums;

namespace YetAnotherAutoChess.Business
{
    class Attack : GameObject
    {
        public Attack(TargetingSystem targetingSystem, DamageType damageType,
        float projectileSpeed, int range, float projectileDamage,
        Spell spellWhichThisProjectileCarry = null, List<Buff> buffs = null)
        {
            _targeting = targetingSystem;
            _damageType = damageType;
            _projectileSpeed = projectileSpeed;
            _range = range;
            _projectileDamage = projectileDamage;
            _spell = spellWhichThisProjectileCarry;
            _buffs = buffs;
            _startingTime = DateTime.Now;
        }

        public void AddSource(Figure source)
        {
            _source = source;
            Position = source.MainViewModel.Position();
        }

        private TargetingSystem _targeting;
        private DamageType _damageType;
        private Figure _target;
        private Figure _source;
        private float _projectileSpeed;
        private int _range;
        public int Range { get => _range; }
        private float _projectileDamage;

        private Spell _spell;
        private List<Buff> _buffs;
        public List<Buff> Buffs { get => _buffs; }

        public Point3D Position;
        private DateTime _startingTime;

        void Start()
        {
            switch (_targeting)
            {
                case TargetingSystem.Target:
                    _target = _source.Target;
                    break;
                case TargetingSystem.Self:
                    _target = _source;
                    break;
                case TargetingSystem.ClosestEnemy:
                case TargetingSystem.HighestEnemyDps:
                case TargetingSystem.LowestAllyHp:
                default:
                    _target = Dijkstra.EnemyInsideRange(_source, _range, -1, -1, _targeting);
                    break;
            }
            if (_projectileSpeed == 0)
                this.Position = _target.MainViewModel.Position();
        }

        // Update is called once per frame
        public override void Update()
        {
            float damageReturn = 0;

            if (Position == _target.MainViewModel.Position() ||
                Polarity(_target.MainViewModel.Position().X - Position.X) != Polarity(_target.MainViewModel.Position().X - _source.MainViewModel.Position().X) ||
                Polarity(_target.MainViewModel.Position().Y - Position.Y) != Polarity(_target.MainViewModel.Position().Y - _source.MainViewModel.Position().Y)) // doubt this will work
            {
                _source.DamageFeedback(_damageType, _target.TakeDamage(_damageType, _projectileDamage, out damageReturn, Buffs));
                if (_spell != null)
                    _spell.Activate(_target, _source, _buffs);

                if (damageReturn > 0)
                {
                    _target.DamageFeedback(DamageType.Magical, _source.TakeDamage(DamageType.True, damageReturn));
                }
                Destroy();
            }

            Position = new Point3D(
                (_target.MainViewModel.Position().X - _source.MainViewModel.Position().X) * _projectileSpeed * Time.TimeDifferenceFromNowInSeconds(_startingTime),
                Position.Y,
                (_target.MainViewModel.Position().X - _source.MainViewModel.Position().X) * _projectileSpeed * Time.TimeDifferenceFromNowInSeconds(_startingTime));
        }

        private int Polarity(double number)
        {
            if (number > 0)
                return 1;
            if (number < 0)
                return -1;
            return 0;
        }
    }
}
