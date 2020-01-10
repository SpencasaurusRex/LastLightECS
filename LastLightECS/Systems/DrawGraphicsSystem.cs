using DefaultEcs;
using DefaultEcs.System;
using System;
using System.Numerics;

namespace LastLightECS.Systems
{
    [With(typeof(Graphics))]
    [With(typeof(WorldPosition))]
    public class DrawGraphicsSystem : AEntitySystem<float>
    {
        World world;

        public DrawGraphicsSystem(World world) : base(world)
        {
            this.world = world;
        }

        protected override void Update(float deltaTime, in Entity entity)
        { 
            ref Graphics graphics = ref entity.Get<Graphics>();
            Vector2 pos = entity.Get<WorldPosition>().Value;

            int x = (int) Math.Round(pos.X * 2);
            int y = (int) Math.Round(pos.Y);
            
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = graphics.Background;
            Console.ForegroundColor = graphics.Foreground;
            
            Console.Write(graphics.Characters);
        }
    }
}
