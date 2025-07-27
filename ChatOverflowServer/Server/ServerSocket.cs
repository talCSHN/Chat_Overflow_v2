using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ServerSocket
    {
        private Socket listener;
        private static List<Socket> clientList = new List<Socket>();
        private static object lockObj = new object();

        public async Task StartListening(int port)
        {
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(IPAddress.Any, port));
            listener.Listen();

            Console.WriteLine($"{port}포트 접속 대기중");
            while (true)
            {
                Socket client = await listener.AcceptAsync();
                if (client != null)
                {
                    Console.WriteLine("클라이언트 접속 완료");
                }
                lock (lockObj)
                {
                    clientList.Add(client);
                }
                //await HandleClientAsync(client);  // 이렇게 하면 현재 클라 처리하는동안 다른 클라이언트 못받음
                _ =  HandleClientAsync(client);
            }
        }

        private async Task HandleClientAsync(Socket client)
        {
            byte[] buf = new byte[1024];
            try
            {
                while (true)
                {
                    int size = await client.ReceiveAsync(buf);
                    if (size == 0) break;

                    string messageFromClient = Encoding.UTF8.GetString(buf, 0, size).Trim();
                    Console.WriteLine($"클라이언트가 보낸 메시지 : {messageFromClient}");

                    Broadcast(messageFromClient);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"서버 수신 오류 : {ex.Message}");
            }
            finally
            {
                lock (lockObj)
                {
                    clientList.Remove(client);
                }
                client.Close();
                Console.WriteLine("서버 - 클라이언트 연결 종료");
            }
        }

        private void Broadcast(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message + "\n");
            lock (lockObj)
            {
                foreach (var c in clientList)
                {
                    try
                    {
                        c.Send(data);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }        
    }
}
