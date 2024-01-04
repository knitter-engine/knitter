using Silk.NET.Vulkan;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Knitter.Common.Asset;

namespace Knitter.Platform.Graphics.Vulkan;

public static class Vulkan_Vertex
{
    public static VertexInputBindingDescription GetBindingDescription()
    {
        VertexInputBindingDescription bindingDescription = new()
        {
            Binding = 0,
            Stride = (uint)Unsafe.SizeOf<Vertex>(),
            InputRate = VertexInputRate.Vertex,
        };

        return bindingDescription;
    }

    public static VertexInputAttributeDescription[] GetAttributeDescriptions()
    {
        var attributeDescriptions = new[]
        {
        new VertexInputAttributeDescription()
        {
            Binding = 0,
            Location = 0,
            Format = Format.R32G32B32Sfloat,
            Offset = (uint)Marshal.OffsetOf<Vertex>(nameof(Vertex.pos)),
        },
        new VertexInputAttributeDescription()
        {
            Binding = 0,
            Location = 1,
            Format = Format.R32G32B32Sfloat,
            Offset = (uint)Marshal.OffsetOf<Vertex>(nameof(Vertex.color)),
        },
        new VertexInputAttributeDescription()
        {
            Binding = 0,
            Location = 2,
            Format = Format.R32G32Sfloat,
            Offset = (uint)Marshal.OffsetOf<Vertex>(nameof(Vertex.textCoord)),
        }
    };

        return attributeDescriptions;
    }
}
