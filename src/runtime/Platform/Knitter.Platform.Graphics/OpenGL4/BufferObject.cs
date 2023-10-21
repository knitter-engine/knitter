using Knitter.Common.Utils;
using Silk.NET.OpenGL;

namespace Knitter.Platform.Graphics.OpenGL;

public class BufferObject<TDataType> : ForceDisposable
    where TDataType : unmanaged
{
    private readonly static GL _gl = GLFactory.GetDefault();

    private uint _handle;
    private BufferTargetARB _bufferType;

    public unsafe BufferObject(Span<TDataType> data, BufferTargetARB bufferType)//TODO:different derived class for different buffer target
    {
        _bufferType = bufferType;
        _handle = _gl.GenBuffer();
        Bind();
        fixed (void* d = data)
        {
            _gl.BufferData(bufferType, (nuint)(data.Length * sizeof(TDataType)), d, BufferUsageARB.StaticDraw);
        }
    }

    public void Bind()
    {
        _gl.BindBuffer(_bufferType, _handle);
    }

    protected override void DoDispose()
    {
        _gl.DeleteBuffer(_handle);
    }
}
