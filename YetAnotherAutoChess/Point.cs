using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess
{
    public class Point
    {
        private int _x;
        public int X { get => _x; set => _x = value; }
        private int _y;
        public int Y { get => _y; set => _y = value; }

        public Point() : this(0, 0)
        {

        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public System.Windows.Point ToWinPoint()
        {
            return new System.Windows.Point(X, Y);
        }
    }
}
