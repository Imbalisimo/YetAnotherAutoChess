using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business
{
    public abstract class GameObject
    {
        public List<System.Windows.FrameworkElement> UIElements;
        protected GameObject()
        {
            LoopManager.AddGameObject(this);
        }

        public abstract void Update();
    }
}
