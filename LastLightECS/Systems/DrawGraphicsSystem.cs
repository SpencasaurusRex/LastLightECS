using DefaultEcs;
using DefaultEcs.System;
using LastLightECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace LastLightECS.Systems
{
    [With(typeof(Graphics))]
    [With(typeof(WorldPosition))]
    public class DrawGraphicsSystem : AEntitySystem<float>
    {
        World world;
        EntitySet screenDirtySet;

        public DrawGraphicsSystem(World world) : base(world)
        {
            this.world = world;
            screenDirtySet = world.GetEntities().With<ScreenDirty>().AsSet();
        }
        
        protected override void Update(float deltaTime, ReadOnlySpan<Entity> entities)
        { 
            // Group togeher graphics on same layer
            List<Tuple<Graphics, Entity>> graphicsEntities = new List<Tuple<Graphics, Entity>>();
            foreach (var entity in entities)
            {
                var g = entity.Get<Graphics>();
                graphicsEntities.Add(new Tuple<Graphics, Entity>(g, entity));
            }
            
            graphicsEntities = graphicsEntities.OrderBy(e => e.Item1.Layer).ToList();

            foreach (var graphic in graphicsEntities)
            {
                Graphics g = graphic.Item1;
                var entity = graphic.Item2;
                Vector2 pos = entity.Get<WorldPosition>().Value;

                int x = (int)Math.Round(pos.X * 2) + Console.WindowWidth / 2;
                int y = (int)Math.Round(pos.Y) + Console.WindowHeight / 2;

                if (x < 0) return;
                if (y < 0) return;
                if (x >= Console.WindowWidth) return;
                if (y >= Console.WindowHeight) return;

                Console.SetCursorPosition(x, y);
                Console.BackgroundColor = g.Background;
                Console.ForegroundColor = g.Foreground;

                Console.Write(g.Characters);

                if (entity.Has<Static>()) continue;

                var dirtyCoords = screenDirtySet.GetEntities().ToArray()[0].Get<ScreenDirty>();
                for (int i = 0; i < g.Characters.Length; i++)
                {
                    dirtyCoords.Add(x + i, y);
                }
            }
        }
    }
}
