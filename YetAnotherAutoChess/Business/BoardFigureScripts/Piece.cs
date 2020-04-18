using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business
{
    public abstract class Piece
    {
        private Enums.Piece _piece;
        public virtual Enums.Piece PieceType { get => _piece; set => _piece = value; }

        private Buff buff;
        public abstract Buff GrantBuff();

        protected bool _toggled = false;

        public bool Toggled { get => _toggled; }

        public void Toggle()
        {
            _toggled = !_toggled;
        }
    }
}
