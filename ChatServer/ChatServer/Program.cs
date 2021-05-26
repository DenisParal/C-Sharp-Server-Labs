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
            public userThread(Socket handler, List<userThread> others)
            {
                thread = new Thread(Work);
                this.handler = handler;
                users = others;

                userLogin = NetStream.RecieveMessage(handler);
                Send(userLogin + " joined the chat");
                NetStream.SendMessage(handler, "Привет, " + userLogin);
                thread.Start(handler);
            }
            protected void Work(object obj)
            {
                Socket handler = (Socket)obj;
                String exitWord = "/Exit";

                while (NetStream.IsConnected(handler))
                {
                    string message = NetStream.RecieveMessage(handler);
                    
                    if (message == exitWord)
                    {
                        NetStream.SendMessage(handler, "You has been disconnected from the chat");
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                        message = "\t" + userLogin + " has been disconnected from the chat";
                        Send(message);
                        break;
                    }
                    else
                    {
                        message = DateTime.Now.ToShortTimeString() + " <" + userLogin + ">: " + message;
                        Console.WriteLine(message);
                        Send(message);
                    }
                }
                Console.WriteLine("User has been disconnected");
            }

            protected void Send(String message)
            {
                foreach (var reciever in users)
                {
                    if (reciever.handler.Connected)
                    {
                        NetStream.SendMessage(reciever.handler, message);
                    }
                }
            }

            ~userThread()
            {
                thread.Join();
            }

            private List<userThread> users;
            public Socket handler;
            protected Thread thread;
            protected String userLogin;
        }

        public ChatServer(String IP = "127.0.0.1", int Port = 8080, int threadCount = 4) : base(IP, Port) 
        { 
            this.threadCount = threadCount;
        }
        public ChatServer(IPAddress IP, int Port = 8080, int threadCount = 4) : base(IP, Port)
        {
            this.threadCount = threadCount;
        }

        protected override void ServerWork()
        {
            Socket handler = socket.Accept();
            for(int i = 0; i< users.Count; i++)
            {
                if (!users[i].handler.Connected)
                {
                    users.Remove(users[i]);
                }
            }
            if (users.Count < threadCount)
            {
                users.Add(new userThread(handler,users));
            }
        }

        ~ChatServer()
        {
            Close();
        }
        protected int threadCount;
        protected List<userThread> users = new List<userThread>();
    }
    class Program
    {
        static void Main(string[] args)
        {
            ChatServer server = new ChatServer();
            server.Start(4);
        }
    }
}
