using Silk.NET.Vulkan;

namespace Knitter.Platform.Graphics.Vulkan;

internal unsafe class Vk_RenderPass(Vk vk, Device device)
    : IRenderPass
{
    private readonly Vk _vk = vk;
    private readonly Device _device = device;
    private RenderPass _renderPass;

    public static IRenderPass? Create(Vk vk, Device device, RenderPassCreateInfo createInfo)
    {
        Vk_RenderPass vkRenderPass = new(vk, device);
        if(vk.CreateRenderPass(device, createInfo, null, out vkRenderPass._renderPass) != Result.Success)
        {
            vkRenderPass.Dispose();
            return null;
        }

        return vkRenderPass;
    }

    public void Dispose()
    {
        _vk.DestroyRenderPass(_device, _renderPass, null);
    }

    public RenderPass GetHandle() => _renderPass;
}
