using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using ServerUtils;

namespace ChatServer
{
    class ChatServer : Server
    {
        protected class userThread
        {
            public userThread(String name, Socket handler)
            {
                thread = new Thread(Work);
                thread.Name = name;
                this.handler = handler;
                userLogin = NetStream.RecieveMessage(handler);
                NetStream.SendMessage(handler, "Hi, " + userLogin + "!");
                thread.Start(handler);
            }
            protected void Work(object obj)
            {
                Socket handler = (Socket)obj;
                String exitWord = "/Exit";

                while (handler.Connected)
                {
                    string message = NetStream.RecieveMessage(handler);
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + message);
                    
                    if (message == exitWord)
                    {
                        NetStream.SendMessage(handler, "Canceling");
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                        break;
                    }
                    else
                    {
                        NetStream.SendMessage(handler, userLogin+": "+message);
                    }
                }
            }
            ~userThread()
            {
                thread.Join();
            }
            public Socket handler;
            protected Thread thread;
            protected String userLogin;
        }
        public ChatServer(String IP = "127.0.0.1", int Port = 8080, int threadCount = 2) : base(IP, Port) { this.threadCount = threadCount; }
        public ChatServer(IPAddress IP, int Port = 8080, int threadCount = 2) : base(IP, Port) { this.threadCount = threadCount; }
        protected override void ServerWork()
        {
            Socket handler = socket.Accept();
            foreach (userThread thread in users)
            {
                if (!thread.handler.Connected)
                {
                    users.Remove(thread);
                }
            }
            if (users.Count < threadCount)
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
            ChatServer server = new ChatServer();
            server.Start(2);
        }
    }
}
