using Knitter;

namespace Base
{
    internal class PrintActor :UserActor
    {
        public override void Update()
        {
            Console.WriteLine($"Hello, I'm PrintActor.Delta time={Time.deltaTime}");
        }
    }
}
