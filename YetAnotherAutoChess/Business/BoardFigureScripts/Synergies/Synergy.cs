using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.BoardFigureScripts.Synergies
{
    class Synergy
    {
        private Enums.Synergy _synergy;
        public Enums.Synergy SynergyName { get => _synergy; set => _synergy = value; }
        private Enums.SynergyType _synergyType;

        private List<int> _minimumNumberOfUnitsForStage;
        // ETC: stage1=8    stage1=13
        //      stage2=16   stage1=26
        //      stage3=24   stage3=39

        public Enums.SynergyBuffTarget SynergyBuffTarget;

        public Synergy(Enums.Synergy synergy, Enums.SynergyType synergyType, Enums.SynergyBuffTarget target,
            int unitNumberStage1, int unitNumberStage2 = 0, int unitNumberStage3 = 0)
        {
            _synergy = synergy;
            _synergyType = synergyType;
            SynergyBuffTarget = target;
            _minimumNumberOfUnitsForStage = new List<int>();
            _minimumNumberOfUnitsForStage.Add(unitNumberStage1);
            if (unitNumberStage2 != 0)
                _minimumNumberOfUnitsForStage.Add(unitNumberStage2);
            if (unitNumberStage3 != 0)
                _minimumNumberOfUnitsForStage.Add(unitNumberStage3);
        }

        public Buff GrantBlessing(int unitNumber)
        {
            Buff blessing = null;
            for (int i = 0; i < _minimumNumberOfUnitsForStage.Count; ++i)
            {
                if (_minimumNumberOfUnitsForStage[i] > unitNumber)
                    return blessing;
                blessing = CreateBlessing(i);
            }
            return CreateBlessing(_minimumNumberOfUnitsForStage.Count);
        }

        private Buff CreateBlessing(int stage)
        {
            return SynergyBuffs.CreateBlessing(_synergy, stage);
        }
    }
}
