﻿using Silk.NET.Core.Native;
using Silk.NET.GLFW;
using Silk.NET.Vulkan;

using Knitter.Common.Utils;
using Knitter.Platform.Graphics;
using Knitter.Platform.Graphics.Vulkan;

namespace Knitter.Platform.Window;

internal unsafe class GlfwWindow_Vulkan : Disposable, IWindow
{
    public readonly Glfw _glfw;//TODO: not public for vulkan
    public  readonly WindowHandle* _window;//TODO: not public for vulkan
    readonly Vk _vk;//TODO: change to use the rhi

    public event Action? OnClose;
    public event Action? OnLogicUpdate;
    public event Action? OnRenderUpdate;
    public event Action<int, int>? OnResize;

    public GlfwWindow_Vulkan(int width, int height, string title)
    {
        _glfw = Glfw.GetApi();
        _glfw.Init();
        _glfw.WindowHint(WindowHintClientApi.ClientApi, ClientApi.NoApi);

        _window = _glfw.CreateWindow(width, height, title, null, null);
        _glfw.MakeContextCurrent(_window);//must make immediately, or the GL may throw exception before call MakeContextCurrent

        _vk = GraphicsLibraryProvider.Vulkan;

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

    public Vk GetGraphicsInterface() => _vk;//TODO: interface

    public void Update(float deltaTime)
    {
        _glfw.PollEvents();

        _glfw.MakeContextCurrent(_window);
        LogicUpdate();//TODO: deltaTime

        RenderUpdate();//TODO: deltaTime
        _glfw.SwapBuffers(_window);
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

    public void Close() => _glfw.SetWindowShouldClose(_window, true);

    public bool IsAlive => !_glfw.WindowShouldClose(_window);

    public IRhi GetRhi()
    {
        var glfwExtensions = _glfw.GetRequiredInstanceExtensions(out uint glfwExtensionCount);
        Vk_Instance rhi = new Vk_Instance(glfwExtensionCount, glfwExtensions);//TODO: use RhiFactory.CreateXXX
        OnResize += rhi.FramebufferResizeCallback;

        VkNonDispatchableHandle handle = new();
        _glfw.CreateWindowSurface(rhi.GetInstance().ToHandle(), _window, null, &handle);

        rhi.CreateWindowSurface(handle);
        GetWindowSize(out int width, out int height);
        rhi.InitVulkan(width, height);

        return rhi;
    }



}
