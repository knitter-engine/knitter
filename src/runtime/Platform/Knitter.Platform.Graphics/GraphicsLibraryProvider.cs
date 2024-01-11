using Knitter.Common.Utils.Exception;
using Silk.NET.OpenGL;
using Silk.NET.Vulkan;

namespace Knitter.Platform.Graphics;

internal static class GraphicsLibraryProvider
{
    private static GL? _opengl;
    public static GL OpenGL
    {
        set => _opengl = value;
        get => _opengl ?? throw new NotInitializeException($"OpenGL is null. You should set before use.");
    }

    public static readonly Vk Vulkan = Vk.GetApi();
}
