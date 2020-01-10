using DefaultEcs;
using DefaultEcs.System;
using LastLightECS.Components;
using System;

namespace LastLightECS.Systems
{
    public class ClearScreenSystem : AComponentSystem<float, ScreenDirty>
    {
        public ClearScreenSystem(World world) : base(world)
        {
        }

        protected override void Update(float deltaTime, ref ScreenDirty screenDirty)
        {
            foreach (var coords in screenDirty.Coords)
            {
                Console.SetCursorPosition(coords.Item1, coords.Item2);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ");
            }
        }
    }
}
