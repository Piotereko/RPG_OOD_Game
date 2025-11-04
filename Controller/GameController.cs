using RPG_wiedzmin_wanna_be.DTO.Commands;
using RPG_wiedzmin_wanna_be.Model;
using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Game;
using RPG_wiedzmin_wanna_be.Model.World;
using RPG_wiedzmin_wanna_be.Network;
using RPG_wiedzmin_wanna_be.View;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace RPG_wiedzmin_wanna_be.Controller
{
    internal class GameController
    {
        private Dungeon dungeon;
        private DungeonBuilder builder;
        private DungeonDirector director;
        private TurnManager turnManager;
        private InputHandlerChain inputHandlerChain;
        private ActionExecutor actionExecutor;

        private Server? server;
        private Client? client;

        private bool isServer;
        private bool isClient;

        public GameController()
        {
            builder = new DungeonBuilder();
            director = new DungeonDirector(builder);
            turnManager = new TurnManager();

            dungeon = builder.Build();
            dungeon = director.CreateTest();

            inputHandlerChain = new InputHandlerChain(dungeon);
            actionExecutor = new ActionExecutor();
        }

        public void Run(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                if (!ParseArgsAndStart(args))
                {
                    Console.WriteLine("Invalid arguments");
                    SelectMode();
                }
            }
            else
            {
                SelectMode();
            }
        }

        private bool ParseArgsAndStart(string[] args)
        {
            try
            {
                if (args[0].Equals("--server", StringComparison.OrdinalIgnoreCase))
                {
                    int port = 5555;
                    if (args.Length > 1 && int.TryParse(args[1], out int p))
                        port = p;

                    isServer = true;
                    StartServer(port);
                    return true;
                }
                else if (args[0].Equals("--client", StringComparison.OrdinalIgnoreCase))
                {
                    string ip = "127.0.0.1";
                    int port = 5555;

                    if (args.Length > 1)
                    {
                        var parts = args[1].Split(':');
                        if (parts.Length >= 1 && !string.IsNullOrWhiteSpace(parts[0]))
                            ip = parts[0];
                        if (parts.Length == 2 && int.TryParse(parts[1], out int p))
                            port = p;
                    }

                    isClient = true;
                    StartClient(ip, port);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public void SelectMode()
        {
            Console.WriteLine("Start as (S)erver or (C)lient?");
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.S)
            {
                isServer = true;
                StartServer(5555); 
            }
            else if (key == ConsoleKey.C)
            {
                isClient = true;
                StartClient("127.0.0.1", 5555); 
            }
            else
            {
                Console.WriteLine("Invalid input, starting as single-player.");
                RunSinglePlayer();
            }
        }

        private void StartServer(int port)
        {
            var initialState = new GameState
            {
                Dungeon = dungeon,
                TurnManager = turnManager
            };

            server = new Server(port, initialState);
            server.Start();

            Console.WriteLine($"Server started on port {port}. Press any key to start game loop.");
            Console.ReadKey();

            //RunServerLoop();
        }

        private void RunServerLoop()
        {
            Console.Clear();
            Console.CursorVisible = false;

            while (true)
            {
                ConsoleView.RenderServer(server.gameState.Dungeon, server.gameState.Players);

                turnManager.UpdateEffects();

                Thread.Sleep(1000);
            }
        }


        private void StartClient(string ip, int port)
        {
            client = new Client(ip, port);

            RunClientLoop();
        }

        private void RunClientLoop()
        {
            Console.Clear();
            Console.CursorVisible = false;
            bool initial = false;

            while (true)
            {
                var gs = client.CurrentGameState;
                if (gs != null && initial == false)
                {
                    //var gs = client.CurrentGameState;
                    ConsoleView.RenderFull(gs.Dungeon, gs.Players, client.local_player_id, gs.TurnManager);
                    initial = true;
                }
                else if (gs == null)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Waiting for game state from server...");
                }
                else
                {
                    //var gs = client.CurrentGameState;

                    ConsoleView.RenderFull(gs.Dungeon, gs.Players, client.local_player_id, gs.TurnManager);
                }

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    PlayerCommand? command = inputHandlerChain.Handle(key.Key);

                    foreach (var enemy in gs.Dungeon.enemies)
                    {
                        if (enemy.IsAlive)
                            enemy.Act(gs.Players[client.local_player_id], dungeon);
                    }

                    if (command != null)
                    {
                        client.SendCommand(command);
                    }
                    else
                    {
                        Logger.PrintLog($"Unhandled key: {key}");
                    }
                }

                Thread.Sleep(200);
            }
        }

        public void Run()
        {
            if (isServer)
                RunServerLoop();
            else if (isClient)
                RunClientLoop();
            else
                RunSinglePlayer();
        }

        private void RunSinglePlayer()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Player player = new Player(0);
            ConsoleView.RenderFull(dungeon, player, turnManager);

            while (true)
            {
                ConsoleView.RenderUpdate(dungeon, player, turnManager);
                

                ConsoleKeyInfo key = Console.ReadKey(true);
                PlayerCommand? command = inputHandlerChain.Handle(key.Key);
                if (command != null)
                {
                    actionExecutor.Execute(command, player, dungeon, turnManager);
                }
                else
                {
                    Logger.PrintLog($"Unhandled key: {key.Key}");
                }
            }
        }
    }

    public class GameState
    {
        public Dungeon Dungeon { get; set; } = null!;
        public Dictionary<int, Player> Players { get; set; } = new Dictionary<int, Player>();
        public TurnManager TurnManager { get; set; } = null!;
    }
}
