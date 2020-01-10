using DefaultEcs;
using DefaultEcs.System;
using LastLightECS.Components;
using System;

namespace LastLightECS.Systems
{
    [With(typeof(InputStroke))]
    public class InputStrokeClearSystem : AEntitySystem<float>
    {
        World world;
        public InputStrokeClearSystem(World world) : base(world)
        {
            this.world = world;
        }

        protected override void Update(float state, ReadOnlySpan<Entity> entities)
        {
            foreach (var entity in entities)
            {
                entity.Dispose();
            }
        }
    }
}
