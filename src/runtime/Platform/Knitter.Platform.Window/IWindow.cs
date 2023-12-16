using Knitter.Platform.Graphics.Vulkan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Knitter.Platform.Window;

public interface IWindow
{
    public event Action<int, int>? OnResize;
    public bool IsAlive { get; }
    public void Update(float deltaTime);
    public void GetWindowSize(out int width, out int height);
    public void Close();

    public HelloTriangleApplication GetRhi();//TODO: return IRhi
}
