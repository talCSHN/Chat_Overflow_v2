using Server;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class LoginServer
{
    static List<Socket> clientList = new List<Socket>();
    static object lockObj = new object();

    static async Task Main(string[] args)
    {
        var server = new ServerSocket();
        await server.StartListening(9000);
        //IPEndPoint serverEP = new IPEndPoint(IPAddress.Any, 9000);
        //Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //serverSocket.Bind(serverEP);
        //serverSocket.Listen(10);

        //Console.WriteLine("로그인 서버 시작");

        //while (true)
        //{
        //    Socket clientSocket = serverSocket.Accept();
        //    lock (lockObj)
        //    {
        //        clientList.Add(clientSocket);
        //    }

        //    Thread clientThread = new Thread(HandleClient);
        //    clientThread.Start(clientSocket);
        //}
    }

    //static void HandleClient(object obj)
    //{
    //    Socket client = (Socket)obj;
    //    byte[] buffer = new byte[1024];

    //    try
    //    {
    //        while (true)
    //        {
    //            int size = client.Receive(buffer);
    //            if (size == 0) break;

    //            string msg = Encoding.UTF8.GetString(buffer, 0, size).Trim();
    //            Console.WriteLine("수신된 메시지: " + msg);

    //            // 모든 클라이언트에게 브로드캐스트
    //            Broadcast(msg);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine("클라이언트 오류: " + ex.Message);
    //    }
    //    finally
    //    {
    //        lock (lockObj)
    //        {
    //            clientList.Remove(client);
    //        }
    //        client.Close();
    //    }
    //}

    //static void Broadcast(string message)
    //{
    //    byte[] data = Encoding.UTF8.GetBytes(message + "\n");
    //    lock (lockObj)
    //    {
    //        foreach (var client in clientList)
    //        {
    //            try
    //            {
    //                client.Send(data);
    //            }
    //            catch { }
    //        }
    //    }
    //}
}
