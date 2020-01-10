using DefaultEcs;
using DefaultEcs.System;
using LastLightECS.Components;
using LastLightECS.Systems;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace LastLightECS
{
    public class Game : IDisposable
    {
        const int FrameMillis = 50;

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
            Console.CursorVisible = false;
            
            systems.Add(new ClearScreenSystem(world));
            systems.Add(new InputStrokeCreatorSystem(world));
            systems.Add(new ShutdownSystem(world));
            systems.Add(new ObjectMoveSystem(world));
            systems.Add(new DrawGraphicsSystem(world));

            var fireball = world.CreateEntity();
            fireball.Set(new WorldPosition{Value = new Vector2(0, 0)});
            fireball.Set(new Velocity{Value = new Vector2(5, 5)});
            fireball.Set(new Graphics
            {
               Foreground = ConsoleColor.Yellow,
               Background = ConsoleColor.Red,
               Characters = "()"
            });

            var globalEntity = world.CreateEntity();
            globalEntity.Set(new ScreenDirty());
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
