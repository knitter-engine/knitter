using System;
using System.Threading;
using System.Threading.Tasks;
using Silk.NET.OpenGL;
using Knitter.Platform.Window;
using Knitter.GameObjects;
using Knitter.Utils;

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

        int width = 1680;
        int height = 920;

        GlfwWindow_OpenGL window = new GlfwWindow_OpenGL(width, height, "Knitter Glfw Window");
        GL gl = window.GetGraphicsInterface();
        //ImGuiController _controller = new ImGuiController(gl, window);

        window.OnRenderUpdate += () =>
        {
           //_controller.Update(15);

            gl.ClearColor(0.05f, 0.05f, 0.05f, 1f);
            //gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            ImGuiNET.ImGui.Text("123");
            ImGuiNET.ImGui.Button("456");
            //_controller.Render();
        };

        while (!window.IsClosed)
        {
            window.Update();
            Thread.Sleep(10);
        }

        window.Close();
        window.Dispose();
    }

    public void Stop()
    {
        _canRun = false;
    }

}
