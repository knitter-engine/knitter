using SixLabors.ImageSharp;
using OpenTK.Graphics.OpenGL4;

namespace Knitter.Platform.Graphics.OpenGL4;

public class Texture : IDisposable
{
    private int _handle;

    public unsafe Texture(string path)
    {
        _handle = GL.GenTexture();
        Bind();

        using (var img = Image.Load<Rgba32>(path))
        {
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, img.Width, img.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, nint.Zero);

            img.ProcessPixelRows(accessor =>
            {
            for (int y = 0; y < accessor.Height; y++)
            {
                fixed (void* data = accessor.GetRowSpan(y))
                    GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, y, accessor.Width, 1, PixelFormat.Rgba, PixelType.UnsignedByte, (nint)data);
                }
            });
        }

        SetParameters();
    }

    //TODO: if nint should be Span<nint>?
    public unsafe Texture(nint data, int width, int height)
    {
        _handle = GL.GenTexture();
        Bind();

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);
        SetParameters();
    }

    private void SetParameters()
    {
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 8);
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
    }

    public void Bind(TextureUnit textureSlot = TextureUnit.Texture0)
    {
        GL.ActiveTexture(textureSlot);
        GL.BindTexture(TextureTarget.Texture2D, _handle);
    }

    public void Dispose()
    {
        GL.DeleteTexture(_handle);
    }
}
