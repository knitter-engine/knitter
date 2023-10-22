using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Knitter.Platform.Window;

public interface IWindow
{
    public event Action? OnResize;
    public void GetWindowSize(out int width, out int height);
}
