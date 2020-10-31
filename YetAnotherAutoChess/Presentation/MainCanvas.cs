using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace YetAnotherAutoChess.Presentation
{
    public static class MainCanvas
    {
        public static Canvas instance;

        public static void setMainCanvas(Canvas canvas)
        {
            instance = canvas;
        }
    }
}
