using Knitter.Common.Asset;
using Knitter.Platform.Graphics;
using Knitter.Platform.Window;
using System.Diagnostics;

namespace TempExample.Demo
{
    internal static class VulkanDemo
    {
        public static void Run()
        {
            Model model = new Model(@"Assets\viking_room.obj");
            Vertex[] vertices = model.vertices!;
            uint[] indices = model.indices!;

            //Create a window.
            IWindow window = WindowFactory.CreateWindows(800, 800, "Vulkan");
            IRhi rhi = window.GetRhi();

            rhi.CreateVertexBuffer(vertices);
            rhi.CreateIndexBuffer(indices);
            rhi.CreateCommandBuffers((uint)model!.indices!.Length);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            float currentUpdateMilliseconds = 0, lastUpdateMilliseconds = -50f;
            while (window.IsAlive)
            {
                currentUpdateMilliseconds = sw.ElapsedMilliseconds;
                float deltaTime = (currentUpdateMilliseconds - lastUpdateMilliseconds) / 1000f;

                lastUpdateMilliseconds = currentUpdateMilliseconds;
                rhi.DrawFrame(sw.ElapsedMilliseconds / 1000);
                window.Update(sw.ElapsedMilliseconds);
            }

            rhi.Dispose();
            window.Close();
        }
    }
}
