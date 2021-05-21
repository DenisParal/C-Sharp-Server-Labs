using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketTCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerEcho server = new ServerEcho();
        }
    }
}
