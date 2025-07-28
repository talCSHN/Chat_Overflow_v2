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
    }
}
