using DefaultEcs;
using DefaultEcs.System;
using System.Numerics;

namespace LastLightECS.Systems
{
    [With(typeof(Velocity))]
    [With(typeof(WorldPosition))]
    public class ObjectMoveSystem : AEntitySystem<float>
    {
        World world;
        public ObjectMoveSystem(World world) : base(world)
        {
            this.world = world;
        }

        protected override void Update(float deltaTime, in Entity entity)
        {
            ref WorldPosition position = ref entity.Get<WorldPosition>();
            Vector2 velocity = entity.Get<Velocity>().Value;

            position.Value += velocity * deltaTime;
        }
    }
}
