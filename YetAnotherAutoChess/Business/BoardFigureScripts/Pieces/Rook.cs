using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.BoardFigureScripts.Pieces
{
    class Rook : Piece
    {
        public Rook()
        {
            _piece = Enums.Piece.Rook;
        }
        private Enums.Piece _piece;
        public override Enums.Piece PieceType { get => _piece; }

        public class RookBuff : Buff
        {
            public RookBuff(BuffProperties stats, float duration) : base(stats, duration)
            {

            }
        }

        public override Buff GrantBuff()
        {
            Buff.BuffProperties properties = new Buff.BuffProperties();
            properties.AttackDamagePercentage = 10;
            if (_toggled)
            {
                properties.ArmorPercentage = 20;
                properties.MagicResistPercentage = 20;
            }
            else
            {
                properties.ArmorPercentage = 50;
                properties.MagicResistPercentage = 50;
                properties.Silenced = 1;
            }

            RookBuff buff = new RookBuff(properties, float.PositiveInfinity);

            return buff;
        }
    }
}
