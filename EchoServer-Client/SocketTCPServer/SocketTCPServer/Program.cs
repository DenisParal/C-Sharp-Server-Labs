using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketTCPServer
{
    class Program
    {
        static int port = 8005;
        static void Main(string[] args)
        {
            ServerEcho server = new ServerEcho();
        }
    }
}
