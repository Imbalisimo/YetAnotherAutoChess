using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business.BoardFigureScripts.Units
{
    interface ICloneable
    {
        Unit Clone(); // Remember to clone MainViewModel too
    }
}
