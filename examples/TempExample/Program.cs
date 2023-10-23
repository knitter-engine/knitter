using Base;
using Knitter;
using Knitter.GameObjects;

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
