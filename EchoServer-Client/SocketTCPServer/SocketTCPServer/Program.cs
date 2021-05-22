using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketTCPServer
{
    class ServerEcho : Server
    {
        public ServerEcho(String IP = "127.0.0.1", int Port = 8080) : base(IP, Port) { }
        public ServerEcho(IPAddress IP, int Port = 8080) : base(IP, Port) { }
        protected override void ServerWork()
        {
            if (handler.Connected)
            {
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                byte[] data = new byte[256];
                do
                {
                    bytes = handler.Receive(data);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (handler.Available > 0);
                Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());

                string message = builder.ToString();
                data = Encoding.Unicode.GetBytes(message);
                handler.Send(data);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ServerEcho server = new ServerEcho();
            server.Start(1);
        }
    }
}
