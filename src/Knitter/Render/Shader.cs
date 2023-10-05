using System;
using System.IO;
using System.Numerics;
using System.Reflection.Metadata;
using OpenTK.Graphics.OpenGL4;

namespace Knitter.Render
{
    public class Shader : IDisposable
    {
        private int _program;

        public Shader(string vertexPath, string fragmentPath)
        {
            int vertex = LoadShader(ShaderType.VertexShader, vertexPath);
            int fragment = LoadShader(ShaderType.FragmentShader, fragmentPath);

            _program = GL.CreateProgram();
            GL.AttachShader(_program, vertex);
            GL.AttachShader(_program, fragment);
            GL.LinkProgram(_program);

            GL.GetProgram(_program, GetProgramParameterName.LinkStatus, out var status);
            if (status == 0)
            {
                throw new Exception($"Program failed to link with error: {GL.GetProgramInfoLog(_program)}");//TODO: internal error
            }

            GL.DetachShader(_program, vertex);
            GL.DetachShader(_program, fragment);
            GL.DeleteShader(vertex);
            GL.DeleteShader(fragment);
        }

        public void Use()
        {
            GL.UseProgram(_program);
        }

        public void SetUniform(string name, int value)
        {
            int location = GL.GetUniformLocation(_program, name);
            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");//TODO: internal error
            }
            GL.Uniform1(location, value);
        }

        public unsafe void SetUniform(string name, Matrix4x4 value)
        {
            //A new overload has been created for setting a uniform so we can use the transform in our shader.
            int location = GL.GetUniformLocation(_program, name);
            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }
            GL.UniformMatrix4(location, 1, false, (float*) &value);
        }

        public void SetUniform(string name, float value)
        {
            int location = GL.GetUniformLocation(_program, name);
            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }
            GL.Uniform1(location, value);
        }


        private int LoadShader(ShaderType type, string path)
        {
            string src = File.ReadAllText(path);
            int handle = GL.CreateShader(type);
            GL.ShaderSource(handle, src);
            GL.CompileShader(handle);

            //GL.GetShader(handle, ShaderParameter.CompileStatus, out int success);
            //if (success == 0)
            //{
            //    string infoLog = GL.GetShaderInfoLog(handle);
            //    Console.WriteLine(infoLog);
            //}
            string infoLog = GL.GetShaderInfoLog(handle);
            if (!string.IsNullOrWhiteSpace(infoLog))
            {
                throw new Exception($"Error compiling shader of type {type}, failed with error {infoLog}");//TODO: internal error
            }

            return handle;
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
                GL.DeleteProgram(_program);
                // end of customize release code

                _disposed = true;
            }
        }

        ~Shader()
        {
            if (!_disposed)
            {
                Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");//TODO: internal log error
            }
        }
        #endregion
    }
}
