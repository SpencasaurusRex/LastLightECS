using DefaultEcs;
using DefaultEcs.System;

namespace LastLightECS.Systems
{
    public class ShutdownSystem : AComponentSystem<float, InputStroke>
    {
        World world;

        public ShutdownSystem(World world) : base(world)
        {
            this.world = world;
        }

        protected override void Update(float deltaTime, ref InputStroke inputStroke)
        {
            if (inputStroke.Info.Key == System.ConsoleKey.Escape)
            {
                var entity = world.CreateEntity();
                entity.Set<Shutdown>();
            }
        }
    }
}