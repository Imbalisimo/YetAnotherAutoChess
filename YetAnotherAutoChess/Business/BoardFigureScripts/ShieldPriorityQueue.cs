using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business
{
    public class ShieldPriorityQueue
    {
        private List<Buff> _queue;
        private float _shieldAllTogether;

        void AddShield(Buff buff, Unit.Properties properties)
        {
            _shieldAllTogether = properties.Shield * ((100 + buff.BuffStats.ShieldPercentage) / 100);
            _shieldAllTogether += buff.BuffStats.ShieldFlat;
        }

        public void Enqueue(Buff buff, Unit.Properties properties)
        {
            AddShield(buff, properties);
            _queue.Add(buff);
            //            Queue.Sort(buff.);
        }
        public float Dequeue(Buff buff, float currentShield)
        {
            if (currentShield == _shieldAllTogether)
            {
                _queue.RemoveAt(0);
                currentShield -= buff.BuffStats.ShieldFlat;
                currentShield /= 1 + buff.BuffStats.ShieldPercentage;
            }
            else
            {
                float difference = _shieldAllTogether - currentShield;
                currentShield -= difference;
                _shieldAllTogether /= 1 + buff.BuffStats.ShieldPercentage;
                _shieldAllTogether -= buff.BuffStats.ShieldFlat;
            }
            return 0;
        }
    }
}
