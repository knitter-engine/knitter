using Knitter.Common.Utils.Exception;
using Silk.NET.OpenGL;
using Silk.NET.Vulkan;

namespace Knitter.Platform.Graphics.Common;

public static class GraphicsApiFactory
{
    private static GL? _opengl;
    public static GL OpenGL
    {
        set => _opengl = value;
        get => _opengl ?? throw new NotInitializeException($"OpenGL is null. You should set before use.");
    }

    private static Vk? _vulkan;
    public static Vk Vulkan
    {
        set => _vulkan = value;
        get => _vulkan ?? throw new NotInitializeException($"Vulkan is null. You should set before use.");
    }
}
