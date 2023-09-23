using System;
using System.Collections.Generic;
using System.Text;

namespace Knitter
{
    public class GameObject
    {
        private List<UserActor> _actors = new List<UserActor>();

        internal void Update()
        {
            _actors.ForEach(o => o.Update());
        }

        public void AddComponent(UserActor actor) => _actors.Add(actor);
        public void RemoveComponent(UserActor actor) => _actors.Remove(actor);
    }
}
