using System.Numerics;
using Knitter.Common.Utils;
using Silk.NET.OpenGL;

namespace Knitter.Platform.Graphics.OpenGL;

public class Shader : ForceDisposable
{
    private readonly static GL _gl = GLFactory.GetDefault();

    private uint _program;

    public Shader(string vertexPath, string fragmentPath)
    {
        uint vertex = LoadShader(ShaderType.VertexShader, vertexPath);
        uint fragment = LoadShader(ShaderType.FragmentShader, fragmentPath);

        _program = _gl.CreateProgram();
        _gl.AttachShader(_program, vertex);
        _gl.AttachShader(_program, fragment);
        _gl.LinkProgram(_program);

        _gl.GetProgram(_program, GLEnum.LinkStatus, out var status);
        if (status == 0)
        {
            throw new Exception($"Program failed to link with error: {_gl.GetProgramInfoLog(_program)}");//TODO: internal error
        }

        _gl.DetachShader(_program, vertex);
        _gl.DetachShader(_program, fragment);
        _gl.DeleteShader(vertex);
        _gl.DeleteShader(fragment);
    }

    public void Use()
    {
        _gl.UseProgram(_program);
    }

    public void SetUniform(string name, int value)
    {
        int location = _gl.GetUniformLocation(_program, name);
        if (location == -1)
        {
            throw new Exception($"{name} uniform not found on shader.");//TODO: internal error
        }
        _gl.Uniform1(location, value);
    }

    public unsafe void SetUniform(string name, Matrix4x4 value)
    {
        //A new overload has been created for setting a uniform so we can use the transform in our shader.
        int location = _gl.GetUniformLocation(_program, name);
        if (location == -1)
        {
            throw new Exception($"{name} uniform not found on shader.");
        }
        _gl.UniformMatrix4(location, 1, false, (float*) &value);
    }

    public void SetUniform(string name, float value)
    {
        int location = _gl.GetUniformLocation(_program, name);
        if (location == -1)
        {
            throw new Exception($"{name} uniform not found on shader.");
        }
        _gl.Uniform1(location, value);
    }

    public int GetAttribLocation(string attribName)
    {
        return _gl.GetAttribLocation(_program, attribName);
    }

    private uint LoadShader(ShaderType type, string path)
    {
        string src = File.ReadAllText(path);
        uint handle = _gl.CreateShader(type);
        _gl.ShaderSource(handle, src);
        _gl.CompileShader(handle);

        //GL.GetShader(handle, ShaderParameter.CompileStatus, out int success);
        //if (success == 0)
        //{
        //    string infoLog = GL.GetShaderInfoLog(handle);
        //    Console.WriteLine(infoLog);
        //}
        string infoLog = _gl.GetShaderInfoLog(handle);
        if (!string.IsNullOrWhiteSpace(infoLog))
        {
            throw new Exception($"Error compiling shader of type {type}, failed with error {infoLog}");//TODO: internal error
        }

        return handle;
    }

    protected override void DoDispose()
    {
        _gl.DeleteProgram(_program);
    }
}
