using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business
{
    class LoopManager
    {
        private static UpdateLoop loop = new UpdateLoop();

        public static void AddGameObject(GameObject obj)
        {
            loop.AddGameObject(obj);
        }
    }
}
