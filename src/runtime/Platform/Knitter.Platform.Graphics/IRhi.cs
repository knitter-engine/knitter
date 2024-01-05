using Knitter.Common.Asset;

namespace Knitter.Platform.Graphics;

public interface IRhi : IDisposable
{
    public void CreateVertexBuffer(Vertex[] vertices);
    public void CreateIndexBuffer(uint[] indices);
    public void CreateTexture(string texturePath);
    public void CreateUniformAndDescriptor();
    public void CreateCommandBuffers(uint indicesLength);
    void DrawFrame(float deltaTime);
}
