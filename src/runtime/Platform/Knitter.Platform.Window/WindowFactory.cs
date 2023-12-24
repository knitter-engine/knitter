namespace Knitter.Platform.Window;

public static class WindowFactory
{
    public static IWindow CreateWindows(int width, int height, string title)
        => new GlfwWindow_Vulkan(width, height, title);
}
