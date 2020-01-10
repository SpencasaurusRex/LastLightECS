using DefaultEcs;
using DefaultEcs.System;
using LastLightECS.Components;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace LastLightECS.Systems
{
    [With(typeof(BoardPosition))]
    [With(typeof(WorldPosition))]
    public class ObjectUpdatePositionSystem : AEntitySystem<float>
    {
        World world;
        Dictionary<Tuple<int,int>, Vector2> boardPositions;

        public ObjectUpdatePositionSystem(World world) : base(world)
        {
            this.world = world;

            // Pre-calculate board position
            boardPositions = new Dictionary<Tuple<int, int>, Vector2>();
            for (int lane = 0; lane < 8; lane++)
            {
                float theta = (float)(lane * 2 * Math.PI / 8f);
                float c = (float)Math.Cos(theta);
                float s = (float)Math.Sin(theta);
                Vector2 delta = new Vector2((float)Math.Round(c), (float)Math.Round(s));

                for (int radius = 0; radius < 8; radius++)
                {
                    boardPositions.Add(new Tuple<int, int>(lane, radius), delta * (radius + 1));
                }
            }
        }

        protected override void Update(float deltaTime, in Entity entity)
        {
            ref var worldPosition = ref entity.Get<WorldPosition>();
            var board = entity.Get<BoardPosition>();
            var boardPos = new Tuple<int, int>(board.Lane, board.Radius);

            worldPosition.Value = boardPositions[boardPos];
        }
    }
}
