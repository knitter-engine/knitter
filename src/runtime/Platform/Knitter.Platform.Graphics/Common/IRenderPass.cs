using Silk.NET.Vulkan;

namespace Knitter.Platform.Graphics;

public interface IRenderPass : IDisposable
{
    public RenderPass GetHandle();//TODO:delete
}
