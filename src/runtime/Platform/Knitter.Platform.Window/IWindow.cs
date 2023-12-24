using Knitter.Platform.Graphics;

namespace Knitter.Platform.Window;

public interface IWindow
{
    public event Action<int, int>? OnResize;
    public bool IsAlive { get; }
    public void Update(float deltaTime);
    public void GetWindowSize(out int width, out int height);
    public void Close();

    public IRhi GetRhi();//TODO: return IRhi
}
