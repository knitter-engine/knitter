using Base;
using Knitter;

World world = new World();
Scene scene = new Scene();
GameObject gameObject = new GameObject();
PrintActor printActor = new PrintActor();

gameObject.AddComponent(printActor);
scene.AddGameObject(gameObject);
world.AddScene(scene);

KnitterEngine engine = new KnitterEngine(world);
engine.Run();
