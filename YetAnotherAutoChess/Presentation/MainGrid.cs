using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace YetAnotherAutoChess.Presentation
{
    static public class MainGrid
    {
        public static Grid instance;

        public static void setMainGrid(Grid mainGrid)
        {
            instance = mainGrid;
        }
    }
}
