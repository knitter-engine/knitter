using System;
using OpenTK.Graphics.OpenGL4;

namespace Knitter.Render
{
    public class BufferObject<TDataType> : IDisposable
        where TDataType : unmanaged
    {
        private int _handle;
        private BufferTarget _bufferType;

        public unsafe BufferObject(TDataType[] data, BufferTarget bufferType)//TODO:different derived class for different buffer target
        {
            _bufferType = bufferType;

            _handle = GL.GenBuffer();
            Bind();
            GL.BufferData(bufferType, data.Length * sizeof(TDataType), data, BufferUsageHint.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(_bufferType, _handle);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(_handle);
        }
    }
}
