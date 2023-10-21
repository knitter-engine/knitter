using Knitter.Common.Utils;
using Silk.NET.OpenGL;

namespace Knitter.Platform.Graphics.OpenGL;

public class VertexArrayObject<TVertexType, TIndexType> : ForceDisposable
    where TVertexType : unmanaged
    where TIndexType : unmanaged
{
    private readonly static GL _gl = GLFactory.GetDefault();
    public readonly uint handle;

    public VertexArrayObject(BufferObject<TVertexType> vbo, BufferObject<TIndexType> ebo)
    {
        handle = _gl.GenVertexArray();
        Bind();
        vbo.Bind();
        ebo.Bind();
    }

    public unsafe void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, uint vertexSize, nint offSet)
    {
        _gl.VertexAttribPointer(index, count, type, false, vertexSize * (uint)sizeof(TVertexType), (void*)(offSet * sizeof(TVertexType)));
        _gl.EnableVertexAttribArray(index);
    }

    public void Bind()
    {
        _gl.BindVertexArray(handle);
    }

    protected override void DoDispose()
    {
        _gl.DeleteVertexArray(handle);
    }
}
