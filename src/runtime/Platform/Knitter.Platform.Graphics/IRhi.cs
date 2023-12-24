namespace Knitter.Platform.Graphics;

public interface IRhi : IDisposable
{
    void DrawFrame(float deltaTime);
}
