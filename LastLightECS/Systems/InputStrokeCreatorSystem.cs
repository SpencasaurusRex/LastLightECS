﻿using DefaultEcs;
using DefaultEcs.System;
using LastLightECS.Components;
using System;

namespace LastLightECS.Systems
{
    public class InputStrokeCreatorSystem : ISystem<float>
    {

        public bool IsEnabled { get; set; }
        World world;

        public void Dispose() { }

        public InputStrokeCreatorSystem(World world)
        {
            this.world = world;
        }

        public void Update(float state)
        {
            while (Console.KeyAvailable)
            {
                var entity = world.CreateEntity();
                entity.Set(new InputStroke { Info = Console.ReadKey(true) });
            }
        }
    }
}
