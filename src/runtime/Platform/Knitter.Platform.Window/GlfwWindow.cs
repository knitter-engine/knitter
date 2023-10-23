using Knitter.Common.Utils;
using Knitter.Platform.Graphics.OpenGL;
using Silk.NET.GLFW;
using Silk.NET.OpenGL;

namespace Knitter.Platform.Window;

public unsafe class GlfwWindow : Disposable, IWindow
{
    readonly Glfw _glfw;
    readonly WindowHandle* _window;
    readonly GL _gl;//TODO: change to use the rhi

    public event Action? OnClose;
    public event Action? OnLogicUpdate;
    public event Action? OnRenderUpdate;
    public event Action? OnResize;

    public GlfwWindow(int width, int height, string title)
    {
        _glfw = Glfw.GetApi();
        _glfw.Init();

        _window = _glfw.CreateWindow(width, height, title, null, null);
        _glfw.MakeContextCurrent(_window);//must make immediately, or the GL may throw exception before call MakeContextCurrent

        _gl = GL.GetApi(new GlfwContext(_glfw, _window));
        GLFactory.SetDefault(_gl);

        _glfw.SetWindowSize(_window, width, height);
        _glfw.SetWindowTitle(_window, title);
    }

    protected override void DoDispose()
    {
        OnClose?.Invoke();

        _glfw.Terminate();
    }

    public void GetWindowSize(out int width, out int height)
    {
        _glfw.GetWindowSize(_window, out width, out height);
    }

    public GL GetGraphicsInterface() => _gl;

    public void Update()
    {
        _glfw.PollEvents();

        _glfw.MakeContextCurrent(_window);
        LogicUpdate();

        RenderUpdate();
        _glfw.SwapBuffers(_window);
    }

    public void Run()
    {
        while (!IsClosed)
        {
            Update();
        }
    }

    void LogicUpdate()
    {
        //_glfw.in

        OnLogicUpdate?.Invoke();
    }

    void RenderUpdate()
    {

        OnRenderUpdate?.Invoke();
    }

    public void Close()
    {
        _glfw.SetWindowShouldClose(_window, true);
    }

    public bool IsClosed => _glfw.WindowShouldClose(_window);



}
