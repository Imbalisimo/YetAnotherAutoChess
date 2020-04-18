using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.GameAssets.BoardAssets
{
    public class FigureGraph
    {
        private Figure[,] _figures = new Figure[9, 8];

        public Figure this[int i, int j]
        {
            get { return _figures[i + 1, j]; }
            set { _figures[i + 1, j] = value; }
        }
    }
}
