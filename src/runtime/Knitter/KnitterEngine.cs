using System;
using System.Threading;
using System.Threading.Tasks;
using Silk.NET.OpenGL;
using Silk.NET.GLFW;
using Silk.NET.Windowing;
using Knitter.Platform.Graphics.OpenGL;
using Knitter.Platform.Window;

namespace Knitter;

public class KnitterEngine
{
    private readonly World _world;
    private bool _canRun;

    public KnitterEngine(World world) {
        //TODO: world should be load from asset
        _world = world;
    }

    public unsafe void Run()//TODO: avoid run twice
    {
        Task.Factory.StartNew(() => {
            _canRun = true;
            while (_canRun)
            {
                Time.Update();
                //TODO: input.Update();
                _world.Update();
                //TODO: render.Update();
                Thread.Sleep((int)Random.Shared.NextInt64(100, 500));
            }
        });

        GlfwWindow window = new GlfwWindow(800, 600, "Knitter Glfw Window");

        while (!window.IsClosed)
        {
            window.Update();
            Thread.Sleep(10);
        }

        window.Close();
    }

    public void Stop()
    {
        _canRun = false;
    }

}
