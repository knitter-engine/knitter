using System;
using OpenTK.Compute.OpenCL;
using OpenTK.Graphics.OpenGL4;

namespace Knitter.Render
{
    public class VertexArrayObject<TVertexType, TIndexType> : IDisposable
        where TVertexType : unmanaged
        where TIndexType : unmanaged
    {
        public readonly int handle;

        public VertexArrayObject(BufferObject<TVertexType> vbo, BufferObject<TIndexType> ebo)
        {
            handle = GL.GenVertexArray();
            Bind();
            vbo.Bind();
            ebo.Bind();
        }

        public unsafe void VertexAttributePointer(int index, int count, VertexAttribPointerType type, int vertexSize, nint offSet)
        {
            GL.VertexAttribPointer(index, count, type, false, vertexSize * sizeof(TVertexType), offSet * sizeof(TVertexType));
            GL.EnableVertexAttribArray(index);
        }

        public void Bind()
        {
            GL.BindVertexArray(handle);
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
                GL.DeleteVertexArray(handle);
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
}
