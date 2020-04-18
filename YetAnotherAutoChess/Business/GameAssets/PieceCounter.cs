using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.GameAssets
{
    class PieceCounter : GameObject
    {
        //public Text Pawns;
        //public Text Rooks;
        //public Text Bishops;
        //public Text Knights;
        //public Text Queens;

        public int[] Cost;

        public PieceCounter()
        {
            Cost = new int[6];
        }

        public void UpdateCostSum(Enums.Piece piece, int cost)
        {
        //    Cost[(int)piece] += cost;
        //    UpdateCostText();
        }

        private void UpdateCostText()
        {
        //    Pawns.text = Cost[(int)Enums.Piece.Pawn].ToString();
        //    Rooks.text = Cost[(int)Enums.Piece.Rook].ToString();
        //    Bishops.text = Cost[(int)Enums.Piece.Bishop].ToString();
        //    Knights.text = Cost[(int)Enums.Piece.Knight].ToString();
        //    Queens.text = Cost[(int)Enums.Piece.Queen].ToString();
        }

        public override void Update()
        {
            
        }
    }
}
