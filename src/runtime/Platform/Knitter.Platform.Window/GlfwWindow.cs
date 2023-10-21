using Knitter.Platform.Graphics.OpenGL;
using Silk.NET.GLFW;
using Silk.NET.OpenGL;
using Silk.NET.SDL;
using Silk.NET.Vulkan;
using Silk.NET.Windowing;

namespace Knitter.Platform.Window;

public unsafe class GlfwWindow
{
    readonly Glfw _glfw;
    readonly WindowHandle* _window;
    readonly GL _gl;//TODO: change to use the rhi

    public event Action? OnCreate;
    public event Action? OnDestroy;
    public event Action? OnLogicUpdate;
    public event Action? OnRenderUpdate;

    public GlfwWindow(int width, int height, string title)
    {
        _glfw = Glfw.GetApi();
        _glfw.Init();

        _window = _glfw.CreateWindow(width, height, title, null, null);
        _gl = GL.GetApi(new GlfwContext(_glfw, _window));
        GLFactory.SetDefault(_gl);

        _glfw.SetWindowSize(_window, width, height);
        _glfw.SetWindowTitle(_window, title);

        OnCreate?.Invoke();
    }

    ~GlfwWindow()
    {
        OnDestroy?.Invoke();

        _glfw.Terminate();
    }

    public GL GetGraphicsInterface() => _gl;

    public void Update()
    {
        LogicUpdate();

        _glfw.MakeContextCurrent(_window);
        RenderUpdate();
        _glfw.SwapBuffers(_window);
    }

    void LogicUpdate()
    {

        OnLogicUpdate?.Invoke();
    }

    float _red = 0f;

    void RenderUpdate()
    {
        _red += 0.01f;
        if (_red > 1f) { _red = 0f; }

        _gl.ClearColor(_red, 0f, 0f, 1f);
        _gl.Clear(ClearBufferMask.ColorBufferBit);

        OnRenderUpdate?.Invoke();
    }

    public void Close()
    {
        _glfw.SetWindowShouldClose(_window, true);
    }

    public bool IsClosed => _glfw.WindowShouldClose(_window);



}
