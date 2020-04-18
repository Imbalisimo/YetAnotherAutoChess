using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.BoardFigureScripts
{
    namespace BuffExtensions
    {
        public interface ISourcable
        {
            void AddSource(Figure figure);
        }

        public interface IStackable
        {
            void SetMaxStacks(int stacks, int damage = 0, Enums.DamageType damageType = Enums.DamageType.none);
            (float, Enums.DamageType) AddStack(); // return damage dealt from stacking
        }

        public interface IDeathInterferable
        {
            bool Interfere(); // return true if death needs to be interupted
        }

        public abstract class BuffWithTimeTrigger : Buff
        {
            public BuffWithTimeTrigger(BuffProperties stats, float duration) : base(stats, duration)
            {

            }

            private DateTime _lastTriggered;
            protected float secondsBewteenTriggers = 0.33f;

            public new void Update()
            {
                Trigger();
                base.Update();
            }

            private void Trigger()
            {
                if (Time.TimeDifferenceFromNowInSeconds(_lastTriggered) < secondsBewteenTriggers)
                    return;

                _lastTriggered = DateTime.Now;
                OnTrigger();
            }

            public abstract void OnTrigger();
        }
    }
}
