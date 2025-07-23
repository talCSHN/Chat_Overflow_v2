using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

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
        Queue<string> TaskQueue = new Queue<string>();
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
        public async Task StartReceiving()
        {
            try
            {
                RecvBuf = new byte[1024];
                int size = socket.Receive(RecvBuf);
                string text = Encoding.UTF8.GetString(RecvBuf, 0, size - 1).Trim();
                TaskQueue.Enqueue(text);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
