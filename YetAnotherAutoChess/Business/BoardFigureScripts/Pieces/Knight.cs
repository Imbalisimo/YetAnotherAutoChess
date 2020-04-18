using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.BoardFigureScripts.Pieces
{
    class Knight : Piece
    {
        public Knight()
        {
            _piece = Enums.Piece.Knight;
        }
        private Enums.Piece _piece;
        public override Enums.Piece PieceType { get => _piece; }

        public class KnightBuff : Buff
        {
            public KnightBuff(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }

        public override Buff GrantBuff()
        {
            Buff.BuffProperties properties = new Buff.BuffProperties();
            properties.AttackSpeedPercentage = 50;
            if (!_toggled)
                properties.RangePercentage = 200;

            KnightBuff buff = new KnightBuff(properties, float.PositiveInfinity);

            return buff;
        }
    }
}
