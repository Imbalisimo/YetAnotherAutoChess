using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.BoardFigureScripts.Pieces
{
    class Bishop : Piece
    {
        public Bishop()
        {
            _piece = Enums.Piece.Bishop;
        }
        private Enums.Piece _piece;
        public override Enums.Piece PieceType { get => _piece; }

        public class BishopBuff : Buff
        {
            public float UnitMana { get; set; }

            public BishopBuff(BuffProperties buffProperties, float duration) : base(buffProperties, duration)
            {

            }

            public override float ApplyToProperties(Unit.Properties property)
            {
                property.StartingMana = property.Mana;

                return base.ApplyToProperties(property);
            }
        }

        public override Buff GrantBuff()
        {
            Buff.BuffProperties properties = new Buff.BuffProperties();

            if (_toggled)
                properties.ManaPercentage = -50;

            BishopBuff buff = new BishopBuff(properties, float.PositiveInfinity);

            if(!_toggled)
                properties.StartingMana = buff.UnitMana;

            return buff;
        }
    }
}
