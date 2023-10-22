using Base;
using Knitter;

World world = new World();
Scene scene = new Scene();
GameObject gameObject = new GameObject();
PrintActor printActor = new PrintActor();

gameObject.AddComponent(printActor);
scene.AddGameObject(gameObject);
world.AddScene(scene);

if (File.Exists("Knitter-logo-128.png"))
{
    Console.WriteLine("file exists");
}

KnitterEngine engine = new KnitterEngine(world);
engine.Run();
