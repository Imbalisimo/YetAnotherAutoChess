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
        public LinkedList<GameObject> objects;
        public UpdateLoop()
        {
            objects = new LinkedList<GameObject>();
            Thread thread = new Thread(Loop);
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }

        public void AddGameObject(GameObject obj)
        {
            objects.AddLast(obj); //Add(obj);
        }

        public void RemoveGameObject(GameObject obj)
        {
            objects.Remove(obj);
        }

        private void Loop()
        {
            while (true)
            {
                LinkedListNode<GameObject> node = objects.Last;
                while(node != null)
                {
                    node.Value.Update();
                    node = node.Previous;
                }
                //for (int i = objects.Count - 1; i >= 0; --i)
                //    objects[i].Update();
            }
        }
    }
}
