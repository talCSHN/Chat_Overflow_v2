//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading.Tasks;

//namespace Server
//{
//    public class LoginServer
//    {
//        private Socket serverSocket;
//        private readonly int port;
//        private static List<Socket> clientList = new List<Socket>();

//        public LoginServer(int port)
//        {
//            this.port = port;
//        }
//        public void LoginServerStart()
//        {
//            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//            serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));

//            while (true)
//            {
//                Socket client = serverSocket.Accept();
//                Console.WriteLine("서버-클라이언트 연결 완료");
//                clientList.Add(client);
//            }
//        }
//    }
//}
