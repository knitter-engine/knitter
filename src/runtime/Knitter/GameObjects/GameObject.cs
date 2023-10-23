using System;
using System.Collections.Generic;
using System.Text;

namespace Knitter.GameObjects;

public class GameObject
{
    private List<UserComponent> _actors = new List<UserComponent>();

    internal void Update()
    {
        _actors.ForEach(o => o.Update());
    }

    public void AddComponent(UserComponent actor) => _actors.Add(actor);
    public void RemoveComponent(UserComponent actor) => _actors.Remove(actor);
}
