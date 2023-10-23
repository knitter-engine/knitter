using Knitter.GameObjects;
using Knitter.Utils;

namespace Base
{
    internal class PrintComponent :UserComponent
    {
        public override void Update()
        {
            Console.WriteLine($"Hello, I'm PrintActor.Delta time={Time.deltaTime}");
        }
    }
}
