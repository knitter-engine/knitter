using Knitter.Platform.Graphics.Vulkan;
using Knitter.Platform.Window;
using Silk.NET.Vulkan;
using System.Diagnostics;

namespace TempExample.Demo
{
    internal static class VulkanDemo
    {
        public static void Run()
        {
            //Create a window.
            IWindow window = new GlfwWindow_Vulkan(HelloTriangleApplication.WIDTH, HelloTriangleApplication.HEIGHT, "Vulkan");
            var app = window.GetRhi();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            float currentUpdateMilliseconds = 0, lastUpdateMilliseconds = -50f;
            while (window.IsAlive)
            {
                currentUpdateMilliseconds = sw.ElapsedMilliseconds;
                float deltaTime = (currentUpdateMilliseconds - lastUpdateMilliseconds) / 1000f;

                lastUpdateMilliseconds = currentUpdateMilliseconds;
                app.DrawFrame(sw.ElapsedMilliseconds / 1000);
                window.Update(sw.ElapsedMilliseconds);
            }
            app.Dispose();
            window.Close();
        }
    }
}
