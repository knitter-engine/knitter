using Silk.NET.OpenGL;
using StbImageSharp;

namespace Knitter.Platform.Graphics.OpenGL;

public class Texture : IDisposable
{
    private readonly static GL _gl = GLFactory.GetDefault();

    private uint _handle;

    public unsafe Texture(string path)
    {
        _handle = _gl.GenTexture();
        Bind();

        ImageResult result = ImageResult.FromMemory(File.ReadAllBytes(path), ColorComponents.RedGreenBlueAlpha);

        fixed (byte* ptr = result.Data)
        {
            // Create our texture and upload the image data.
            _gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)result.Width,
                (uint)result.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
        }

        SetParameters();
    }

    //TODO: if nint should be Span<nint>?
    public unsafe Texture(Span<byte> data, uint width, uint height)
    {
        _handle = _gl.GenTexture();
        Bind();

        fixed (void* d = &data[0])
        {
            //Setting the data of a texture.
            _gl.TexImage2D(TextureTarget.Texture2D, 0, (int)InternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, d);
            SetParameters();
        }
    }

    private void SetParameters()
    {
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 8);
        _gl.GenerateMipmap(TextureTarget.Texture2D);
    }

    public void Bind(TextureUnit textureSlot = TextureUnit.Texture0)
    {
        _gl.ActiveTexture(textureSlot);
        _gl.BindTexture(TextureTarget.Texture2D, _handle);
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
            _gl.DeleteTexture(_handle);
            // end of customize release code

            _disposed = true;
        }
    }

    ~Texture()
    {
        if (!_disposed)
        {
            Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");//TODO: internal log error
        }
    }
    #endregion
}
