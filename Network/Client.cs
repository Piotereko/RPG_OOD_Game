using RPG_wiedzmin_wanna_be.Controller;
using RPG_wiedzmin_wanna_be.DTO.Commands;
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
        private readonly byte[] buffer = new byte[4096];

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
                    byte[] lengthBytes = new byte[4];
                    int read = 0;
                    while (read < 4)
                    {
                        int bytesRead = stream.Read(lengthBytes, read, 4 - read);
                        if (bytesRead == 0) throw new Exception("Disconnected");
                        read += bytesRead;
                    }
                    int messageLength = BitConverter.ToInt32(lengthBytes, 0);

                    
                    byte[] data = new byte[messageLength];
                    read = 0;
                    while (read < messageLength)
                    {
                        int bytesRead = stream.Read(data, read, messageLength - read);
                        if (bytesRead == 0) throw new Exception("Disconnected");
                        read += bytesRead;
                    }

                    string json = Encoding.UTF8.GetString(data);

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

                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Disconnected or error: {ex.Message}");
            }
        }

        public void SendCommand(PlayerCommand command)
        {
            string json = JsonSerializer.Serialize(command);
            byte[] data = Encoding.UTF8.GetBytes(json);
            byte[] lengthPrefix = BitConverter.GetBytes(data.Length);

            try
            {
                stream.Write(lengthPrefix, 0, lengthPrefix.Length);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send command: {ex.Message}");
            }
        }
    }
}
