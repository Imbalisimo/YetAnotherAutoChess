using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.BoardFigureScripts.Pieces
{
    class Pawn : Piece
    {
        public Pawn()
        {
            _piece = Enums.Piece.Pawn;
        }
        private Enums.Piece _piece;
        public override Enums.Piece PieceType { get => _piece; }

        public override Buff GrantBuff()
        {
            return null;
        }
    }
}
