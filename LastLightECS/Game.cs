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
            while (Update()) { }
        }

        void Init()
        { 
            Console.CursorVisible = false;
            Console.WindowWidth = 50*2;
            Console.WindowHeight = 28;
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);

            systems.Add(new ClearScreenSystem(world));
            systems.Add(new InputStrokeCreatorSystem(world));
            systems.Add(new ShutdownSystem(world));
            systems.Add(new InputPlayerMoveSystem(world));
            systems.Add(new ObjectUpdatePositionSystem(world));
            systems.Add(new DrawGraphicsSystem(world));
            systems.Add(new InputStrokeClearSystem(world));

            var playerEntity = world.CreateEntity();
            playerEntity.Set(new Player());
            playerEntity.Set(new BoardPosition{ Lane = 6, Radius = 0 });
            playerEntity.Set(new WorldPosition());
            playerEntity.Set(new Graphics
            {
                Background = ConsoleColor.Black,
                Foreground = ConsoleColor.White,
                Characters = "[]",
                Layer = 1
            });

            var campfire = world.CreateEntity();
            campfire.Set(new WorldPosition { Value = new Vector2(0, 0) });
            campfire.Set(new Graphics
            {
                Background = ConsoleColor.Black,
                Foreground = ConsoleColor.Yellow,
                Characters = "╝╚"
            });

            var globalEntity = world.CreateEntity();
            globalEntity.Set(new ScreenDirty());
            globalEntity.Set(new Board());

            for (int lane = 0; lane < 8; lane++)
            {
                for (int radius = 0; radius < 8; radius++)
                {
                    var boardSlot = world.CreateEntity();
                    boardSlot.Set(new WorldPosition());
                    boardSlot.Set(new BoardPosition { Lane = lane, Radius = radius });
                    boardSlot.Set(new Static());
                    boardSlot.Set(new Graphics
                    {
                        Background = ConsoleColor.Black,
                        Foreground = ConsoleColor.Gray,
                        Characters = "░░"
                    });
                }
            }
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
