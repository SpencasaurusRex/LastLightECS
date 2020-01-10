using DefaultEcs;
using DefaultEcs.System;

namespace LastLightECS.Systems
{
    public class InputStrokeCreatorSystem : ISystem<float>
    {

        public bool IsEnabled { get; set; }
        World world;

        public void Dispose()
        {
        }

        public InputStrokeCreatorSystem(World world)
        {
            this.world = world;
        }

        public void Update(float state)
        {
            while (System.Console.KeyAvailable)
            {
                var entity = world.CreateEntity();
                entity.Set(new InputStroke { Info = System.Console.ReadKey(true) });
            }
        }
    }
}
