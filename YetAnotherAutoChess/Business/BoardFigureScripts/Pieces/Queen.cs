using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.BoardFigureScripts.Pieces
{
    class Queen : Piece
    {
        public Queen()
        {
            _piece = Enums.Piece.Queen;
        }
        private Enums.Piece _piece;
        public override Enums.Piece PieceType { get => _piece; }

        public class QueenBuff : Buff
        {
            public QueenBuff(BuffProperties stats, float duration) : base(stats, duration)
            {

            }

            public override float ApplyToProperties(Unit.Properties property)
            {
                if (this.BuffStats.ShieldFlat == 1)
                {
                    property.Shield += property.Health;
                }

                return base.ApplyToProperties(property);
            }
        }



        public override Buff GrantBuff()
        {
            Buff.BuffProperties properties = new Buff.BuffProperties();
            properties.AttackDamagePercentage = 10;
            if (_toggled)
            {
                properties.ArmorPercentage = 50;
                properties.MagicResistPercentage = 50;
                properties.AbilityPowerPercentage = 50;
                properties.AttackDamagePercentage = 50;
                properties.AttackSpeedPercentage = 25;
                properties.HealthFlat += 250;
            }
            else
            {
                properties.ShieldFlat = 1;

            }

            QueenBuff buff = new QueenBuff(properties, float.PositiveInfinity);

            return buff;
        }
    }
}
