using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Business
{
    class UpdateLoop
    {
        public List<GameObject> objects;
        public UpdateLoop()
        {
            objects = new List<GameObject>();
            Thread thread = new Thread(Loop);
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }

        public void AddGameObject(GameObject obj)
        {
            objects.Add(obj);
        }

        private void Loop()
        {
            while (true)
            {
                for (int i = objects.Count - 1; i >= 0; --i)
                    objects[i].Update();
            }
        }
    }
}
