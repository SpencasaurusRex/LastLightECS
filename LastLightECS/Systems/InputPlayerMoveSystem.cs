using DefaultEcs;
using DefaultEcs.System;
using LastLightECS.Components;
using System;

namespace LastLightECS.Systems
{
    public class InputPlayerMoveSystem : AComponentSystem<float, InputStroke>
    {
        World world;
        EntitySet playerSet;

        public InputPlayerMoveSystem(World world) : base(world)
        {
            this.world = world;
            playerSet = world.GetEntities().With<Player>().With<BoardPosition>().AsSet();
        }

        protected override void Update(float state, Span<InputStroke> components)
        {
            int delta = 0;

            foreach (var input in components)
            {
                if (input.Info.Key == ConsoleKey.A)
                {
                    delta--;
                }
                if (input.Info.Key == ConsoleKey.D)
                {
                    delta++;
                }
            }

            if (delta == 0) return;

            ref var playerPosition = ref playerSet.GetEntities().ToArray()[0].Get<BoardPosition>();
            
            int newLane = playerPosition.Lane + delta;
            newLane %= 8;
            if (newLane < 0) newLane += 8;

            playerPosition.Lane = newLane;
        }
    }
}
