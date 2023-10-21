using Silk.NET.OpenGL;

namespace Knitter.Platform.Graphics.OpenGL;

public class VertexArrayObject<TVertexType, TIndexType> : IDisposable
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

    #region Dispose
    private bool _disposed = false;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            // customize release code
            _gl.DeleteVertexArray(handle);
            // end of customize release code

            _disposed = true;
        }
    }

    ~VertexArrayObject()
    {
        if (!_disposed)
        {
            Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");//TODO: internal log error
        }
    }
    #endregion
}
