using DefaultEcs;
using DefaultEcs.System;
using LastLightECS.Systems;
using System;
using System.Collections.Generic;
using System.Threading;

namespace LastLightECS
{
    public class Game : IDisposable
    {
        const int FrameMillis = 16;

        World world;
        List<ISystem<float>> systems;
        EntitySet shutdownSet;

        public Game()
        { 
            world = new World();
            systems = new List<ISystem<float>>();
            shutdownSet = world.GetEntities().With<Shutdown>().AsSet();
        }

        public void Dispose()
        {
            foreach (var system in systems)
            {
                system.Dispose();
            }
            world.Dispose();
        }

        public void Start()
        { 
            Init();
            while (Update())
            {
            }
        }

        void Init()
        { 
            systems.Add(new InputStrokeCreatorSystem(world));
            systems.Add(new ShutdownSystem(world));
            systems.Add(new DrawGraphicsSystem(world));

            Console.CursorVisible = false;
        }

        bool Update()
        {
            const float deltaTime = FrameMillis / 1000f;

            foreach (var system in systems)
            {
                system.Update(deltaTime);
            }

            Thread.Sleep(FrameMillis);

            return shutdownSet.GetEntities().IsEmpty;
        }
    }
}
