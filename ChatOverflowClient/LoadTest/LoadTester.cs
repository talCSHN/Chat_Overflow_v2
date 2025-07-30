using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class LoadTester
    {
        public async Task Run(int clientCount)
        {
            List<Task> clients = new List<Task>();

            for (int i = 0; i < clientCount; i++)
            {
                int userIndex = i;
                clients.Add(Task.Run(async () =>
                {
                    try
                    {
                        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        await socket.ConnectAsync(IPAddress.Loopback, 9000);

                        string loginMsg = $"LOGIN TestUser{userIndex} {userIndex}\n";
                        byte[] data = Encoding.UTF8.GetBytes(loginMsg);
                        socket.Send(data);

                        string chatMsg = $"CHAT TestUser{userIndex} {userIndex} Test Running\n";
                        socket.Send(Encoding.UTF8.GetBytes(chatMsg));

                        byte[] buf = new byte[1024];
                        int recv = socket.Receive(buf);
                        string response = Encoding.UTF8.GetString(buf, 0, recv);
                        Console.WriteLine($"{userIndex} : {response}");

                        socket.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{userIndex}오류 : {ex.Message}");
                    }
                }));
            }

            await Task.WhenAll(clients);
        }
    }

}
