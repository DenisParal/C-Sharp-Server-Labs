using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ProtocolServer
{
    class ProtocolServer : ServerUtils.Server
    {
        public ProtocolServer(String IP = "127.0.0.1", int Port = 8080) : base(IP, Port) { }
        public ProtocolServer(IPAddress IP, int Port = 8080) : base(IP, Port) { }
        protected override void ServerWork()
        {
            String exitWord = "Exit";
            if (handler.Connected)
            {
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                byte[] data = new byte[256];
                do
                {
                    if(!handler.Connected)
                    {
                        break;
                    }
                    bytes = handler.Receive(data);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (handler.Available > 0);
                Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());
                string message = builder.ToString();
                if (message == exitWord)
                {
                    data = Encoding.Unicode.GetBytes("Canceling connection...");
                    handler.Send(data);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
                else
                {
                    data = Encoding.Unicode.GetBytes(message);
                    handler.Send(data);
                }
            }
            else
            {
                handler = socket.Accept();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ProtocolServer server = new ProtocolServer();
            server.Start(1);
        }
    }
}
