using System;
using System.Threading;
using System.Threading.Tasks;
using Silk.NET.OpenGL;
using Silk.NET.GLFW;
using Silk.NET.Windowing;

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

        //GameMainWindow game = new GameMainWindow(800, 600, "LearnOpenTK");

        //game.Run();
        IWindow window = Window.Create(WindowOptions.Default);
        Glfw glfw = Glfw.GetApi();
        glfw.Init();
        GL gl = GL.GetApi(window);

        WindowHandle* glfwWin = glfw.CreateWindow(800, 600, "Learn silk.net", null, null);
        while (!glfw.WindowShouldClose(glfwWin))
        {
            

            glfw.SwapBuffers(glfwWin);
            Thread.Sleep(100);
        }


    }

    public void Stop()
    {
        _canRun = false;
    }

}
