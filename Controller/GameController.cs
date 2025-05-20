using RPG_wiedzmin_wanna_be.DTO.Commands;
using RPG_wiedzmin_wanna_be.Model;
using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Game;
using RPG_wiedzmin_wanna_be.Model.World;
using RPG_wiedzmin_wanna_be.Network;
using RPG_wiedzmin_wanna_be.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Controller
{
    internal class GameController
    {
        private Dungeon dungeon;
        //private Player player;
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

        public void SelectMode()
        {
            Console.WriteLine("Start as (S)erver or (C)lient?");
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.S)
            {
                isServer = true;
                StartServer();
            }
            else if (key == ConsoleKey.C)
            {
                isClient = true;
                StartClient();
            }
            else
            {
                Console.WriteLine("Invalid input, starting as single-player.");
                RunSinglePlayer();
            }
        }

        private void StartServer()
        {
            var initialState = new GameState
            {
                Dungeon = dungeon,
                
                TurnManager = turnManager
            };

            server = new Server(5555, initialState);
            server.Start();

            Console.WriteLine("Server started. Press any key to start game loop.");
            Console.ReadKey();

            RunServerLoop();
        }

        private void RunServerLoop()
        {
            Console.Clear();
            Console.CursorVisible = false;

            while (true)
            {
                //view.RenderServer(server.gameState.Dungeon, server.gameState.Players);
                turnManager.UpdateEffects();

                Thread.Sleep(100); 
            }
        }

        private void StartClient()
        {
            Console.WriteLine("Enter server IP (default 127.0.0.1):");
            var ip = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ip))
                ip = "127.0.0.1";

            client = new Client(ip, 5555);

            RunClientLoop();
        }

        private void RunClientLoop()
        {
            Console.Clear();
            Console.CursorVisible = false;

            while (true)
            {
                if (client?.CurrentGameState != null)
                {
                    var gs = client.CurrentGameState;
                    ConsoleView.RenderFull(gs.Dungeon, gs.Players,client.local_player_id, gs.TurnManager);
                }
                else
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Waiting for game state from server...");
                }

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    var command = inputHandlerChain.Handle(key);

                    if (command != null)
                    {
                        client.SendCommand(command);
                    }
                    else
                    {
                        Logger.PrintLog($"Unhandled key: {key}");
                    }
                }

                Thread.Sleep(50); 
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
                turnManager.UpdateEffects();

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
            public Dictionary<int,Player> Players { get; set; } = new Dictionary<int, Player>();
            public TurnManager TurnManager { get; set; } = null!;
    }
}
