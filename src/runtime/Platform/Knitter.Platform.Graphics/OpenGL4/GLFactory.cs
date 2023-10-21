using Silk.NET.OpenGL;
using Knitter.Common.Utils.Exception;

namespace Knitter.Platform.Graphics.OpenGL
{
    public static class GLFactory
    {
        private static GL? _default;
        public static void SetDefault(GL gl) => _default = gl;
        internal static GL GetDefault() => _default ?? throw new NotInitializeException($"OpenGL not init. You should call {nameof(SetDefault)}() before use.");
    }
}
