using Base;
using Knitter.GameObjects;
using Knitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempExample.Demo
{
    internal static class KnitterEngineDemo
    {
        public static void Run()
        {
            World world = new World();
            Scene scene = new Scene();
            GameObject gameObject = new GameObject();
            PrintComponent printActor = new PrintComponent();

            gameObject.AddComponent(printActor);
            scene.AddGameObject(gameObject);
            world.AddScene(scene);

            if (File.Exists("Knitter-logo-128.png"))
            {
                Console.WriteLine("file exists");
            }

            KnitterEngine engine = new KnitterEngine(world);
            engine.Run();
        }
    }
}
