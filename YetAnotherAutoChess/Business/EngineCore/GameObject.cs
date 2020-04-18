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
        private MainViewModel _mainViewModel;
        internal MainViewModel MainViewModel { get => _mainViewModel; set => _mainViewModel = value; }

        protected GameObject()
        {
            UIElements = new List<System.Windows.FrameworkElement>();
            LoopManager.AddGameObject(this);
        }

        public abstract void Update();

        public void Destroy()
        {
            _mainViewModel = null;
            UIElements.Clear();
        }
    }
}
