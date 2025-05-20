using RPG_wiedzmin_wanna_be.Controller;
using RPG_wiedzmin_wanna_be.DTO.Commands;
using RPG_wiedzmin_wanna_be.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static RPG_wiedzmin_wanna_be.Network.ClientHandler;

namespace RPG_wiedzmin_wanna_be.Network
{
    internal class Client
    {
        private readonly TcpClient tcpClient;
        private readonly NetworkStream stream;
        private readonly byte[] buffer = new byte[262144];

        public GameState CurrentGameState { get; private set; }
        public int local_player_id { get; private set; }

        public Client(string ip, int port)
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(ip, port);
            stream = tcpClient.GetStream();

            var listenThread = new Thread(ListenForServerMessages);
            listenThread.Start();
        }

        private void ListenForServerMessages()
        {
            try
            {
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string json = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    try
                    {
                        var initData = JsonHelper.Deserialize<InitialConnectionData>(json);
                        if (initData != null && initData.GameState != null)
                        {
                            local_player_id = initData.playerId;
                            CurrentGameState = initData.GameState;
                            continue;
                        }
                    }
                    catch
                    {
                        
                    }

                    CurrentGameState = JsonHelper.Deserialize<GameState>(json);


                    ConsoleView.RenderUpdate(CurrentGameState.Dungeon, CurrentGameState.Players, local_player_id, CurrentGameState.TurnManager);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Disconnected or error: {ex.Message}");
            }
        }

        public void SendCommand(PlayerCommand command)
        {
            command.PlayerId = local_player_id;
            string json = JsonHelper.SerializeObject(command);
            byte[] data = Encoding.UTF8.GetBytes(json);

            try
            {
                stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send command: {ex.Message}");
            }
        }
    }
}
