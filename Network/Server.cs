using RPG_wiedzmin_wanna_be.Controller;
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using RPG_wiedzmin_wanna_be.DTO.Commands;
using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model;

namespace RPG_wiedzmin_wanna_be.Network
{
    public class Server
    {
        private readonly int port;
        private TcpListener listener;
        public List<ClientHandler> clients = new();
        public object clientsLock = new();


        public GameState gameState;
        private ActionExecutor actionExecutor;

        public Server(int _port, GameState initialState) 
        {
            port = _port;
            gameState = initialState;
            actionExecutor = new ActionExecutor();

        }

        public void Start()
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            var acceptThread = new Thread(AcceptClientsLoop);
            acceptThread.Start();
        }

        private void AcceptClientsLoop()
        {
            while (true)
            {
                var tcpClient = listener.AcceptTcpClient();
                lock (clientsLock)
                {
                    if (clients.Count >= 9)
                    {
                        Console.WriteLine("Max clients reached, rejecting connection.");
                        tcpClient.Close();
                        continue;
                    }

                    var playerId = clients.Count + 1;

                    Player new_player = new Player(playerId);
                    lock (gameState)
                    {
                        gameState.Players[playerId] = new_player;
                    }
                    

                    var handler = new ClientHandler(tcpClient, playerId, this);
                    clients.Add(handler);

                    var clientThread = new Thread(handler.HandleClient);
                    clientThread.Start();

                    Console.WriteLine($"Client {playerId} connected.");
                }
            }
        }

        public void BroadcastGameState()
        {
            string json = JsonHelper.SerializeObject(gameState);
            byte[] buffer = Encoding.UTF8.GetBytes(json);

            lock (clientsLock)
            {
                foreach (var client in clients)
                {
                    client.Send(buffer);
                }
            }
        }

        public void ProcessCommand(int playerId, PlayerCommand command)
        {
            Console.WriteLine($"Received command from player {playerId}: {command.Type}");

            Player? player;
            lock (gameState)
            {
                if (!gameState.Players.TryGetValue(playerId, out player))
                {
                    Console.WriteLine($"Player {playerId} not found");
                    return;
                }
            }


            actionExecutor.Execute(command, player, gameState.Dungeon, gameState.TurnManager);
            BroadcastGameState();
        }


    }

    public class ClientHandler
    {
        private readonly TcpClient tcpClient;
        private readonly NetworkStream stream;
        private readonly int playerId;
        private readonly Server server;

        private readonly byte[] buffer = new byte[262144];

        public ClientHandler(TcpClient tcpClient, int playerId, Server server)
        {
            this.tcpClient = tcpClient;
            this.playerId = playerId;
            this.server = server;
            this.stream = tcpClient.GetStream();
        }

        public void HandleClient()
        {
            try
            {
              
                SendInitialGameState();

                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break; 

                    string json = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    var command = JsonHelper.Deserialize<PlayerCommand>(json);

                    if (command != null)
                    {
                        server.ProcessCommand(playerId, command);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Client {playerId} disconnected or error: {ex.Message}");
            }
            finally
            {
                lock (server.gameState)
                {
                    if (server.gameState.Players.ContainsKey(playerId))
                    {
                        server.gameState.Players.Remove(playerId);
                        Console.WriteLine($"Player {playerId} removed.");
                    }
                }
                lock (server.clientsLock)
                {
                    server.clients.Remove(this);
                    Console.WriteLine($"ClientHandler for player {playerId} removed.");
                }
                tcpClient.Close();
                
            }
        }

        public void SendInitialGameState()
        {
            var init_data = new InitialConnectionData
            {
                playerId = playerId,
                GameState = server.gameState

        };
            string json = JsonHelper.SerializeObject(init_data);
            byte[] data = Encoding.UTF8.GetBytes(json);
            stream.Write(data, 0, data.Length);
        }

    internal class InitialConnectionData
    {
        public int playerId { get; set; }
        public GameState GameState { get; set; }
    }

    public void Send(byte[] data)
        {
            try
            {
                stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending data to player {playerId}: {ex.Message}");
            }
        }
    }
}
