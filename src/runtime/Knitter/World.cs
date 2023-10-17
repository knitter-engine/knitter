using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Knitter;

public class World
{
    private List<Scene> _scenes = new List<Scene>();

    internal void Update()
    {
        _scenes.ForEach(s => s.Update());
    }

    public void AddScene(Scene scene) => _scenes.Add(scene);
    public void RemoveScene(Scene scene) => _scenes.Remove(scene);
}
