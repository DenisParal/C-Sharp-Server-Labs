using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MultiClientServer
{
    class ProtocolMultiServer : ServerUtils.Server
    {
        protected class userThread
        {
            public userThread(String name, Socket handler)
            {
                thread = new Thread(Work);
                thread.Name = name;
                this.handler = handler;
                thread.Start(handler);
            }
            protected void Work(object obj)
            {
                Socket handler = (Socket)obj;
                String exitWord = "Exit";
                while (handler.Connected)
                {
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256];
                    do
                    {
                        if (!handler.Connected)
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
            }
            ~userThread()
            {
                thread.Join();
            }
            public Socket handler;
            protected Thread thread;
        }
        public ProtocolMultiServer(String IP = "127.0.0.1", int Port = 8080, int threadCount = 2) : base(IP, Port) { this.threadCount = threadCount; }
        public ProtocolMultiServer(IPAddress IP, int Port = 8080, int threadCount = 2) : base(IP, Port) { this.threadCount = threadCount; }
        protected override void ServerWork()
        {
            Socket handler = socket.Accept();
            foreach(userThread thread in users)
            {
                if(!thread.handler.Connected)
                {
                    users.Remove(thread);
                }
            }
            if(users.Count < threadCount)
            {
                users.Add(new userThread("Thread", handler));
            }
        }
        
        protected int threadCount;
        protected List<userThread> users = new List<userThread>();
    }
    class Program
    {
        static void Main(string[] args)
        {
            ProtocolMultiServer server = new ProtocolMultiServer();
            server.Start(2);
        }
    }
}
