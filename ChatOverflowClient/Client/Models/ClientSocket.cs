using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Client.Models
{
    public class ClientSocket
    {
        private ClientSocket()
        {

        }

        private Socket socket;
        private static ClientSocket instance = new ClientSocket();
        public static ClientSocket Instance
        {
            get => instance;
        }
        
        private byte[] sendBuf;
        public byte[] SendBuf
        {
            get => sendBuf;
            set => sendBuf = value;
        }
        private byte[] recvBuf;
        public byte[] RecvBuf
        {
            get => recvBuf;
            set => recvBuf = value;
        }

        public event Action<string> MessageReceived;

        public async Task MakeConnection(string ip, int port)
        {
            await Task.Run(async () =>
            {
                try
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    EndPoint serverEP = new IPEndPoint(IPAddress.Parse(ip), port);
                    socket.Connect(serverEP);
                    await StartReceiving();
                }
                catch (Exception ex)
                {
                }
            });
        }
        public async Task SendToSever(string message)
        {
            try
            {
                SendBuf = Encoding.UTF8.GetBytes(message);
                await socket.SendAsync(new ArraySegment<byte>(SendBuf));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Send Error: {ex.Message}");
            }
        }
        public async Task StartReceiving()
        {
            while (true)
            {
                try
                {
                    RecvBuf = new byte[1024];
                    int size = await socket.ReceiveAsync(new ArraySegment<byte>(RecvBuf), SocketFlags.None);
                    if (size == 0)
                    {
                        Console.WriteLine("Connection Closed");
                        break;
                    }
                    string text = Encoding.UTF8.GetString(RecvBuf, 0, size - 1).Trim();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageReceived?.Invoke(text);
                    });                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Receive Error: {ex.Message}");
                }
            }      
        }
    }
}
