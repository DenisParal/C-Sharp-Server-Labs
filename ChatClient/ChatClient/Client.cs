﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClient
{
    class Client
    {
        class Reciever
        {
            public Reciever(Socket socket)
            {
                this.socket = socket;
                thread = new Thread(Work);
                thread.Start(socket);
            }
            private void Work(object obj)
            {
                Socket socket = (Socket)obj;
                byte[] data;
                StringBuilder builder;
                while (socket.Connected)
                {
                    data = new byte[256];
                    builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        if(!socket.Connected)
                        {
                            break;
                        }
                        bytes = socket.Receive(data, data.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (socket.Available > 0);
                    mutex.WaitOne();
                    messageList.Add(builder.ToString());
                    mutex.ReleaseMutex();
                }
            }
            public List<String> Get()
            {
                mutex.WaitOne();
                List<String> tmp = new List<String>(messageList);
                messageList.Clear();
                mutex.ReleaseMutex();
                return tmp;
            }
            public void Stop()
            {
                thread.Join();
            }
            ~Reciever()
            {
                Stop();
            }
            private Socket socket;
            private Thread thread;
            public List<String> messageList = new List<string>();
            private Mutex mutex = new Mutex();
        }
        public Client(String login)
        {
            this.login = login;
        }
        public void Connect(String IP, int Port)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
            socket.Connect(ipPoint);
            SendMessage(login);
            reciever = new Reciever(socket);
        }
        public void Disconnect()
        {
            socket.Disconnect(true);
            reciever.Stop();
        }
        public String RecieveMessage()
        {
            List<String> data = reciever.Get();
            String result = "";
            foreach (var line in data)
            {
                result += line + "\n";
            }
            return result;
        }
        public void SendMessage(String message)
        {
            byte[] sentData = Encoding.Unicode.GetBytes(message);
            socket.Send(sentData);
        }
        public bool Connected
        {
            get { return socket.Connected; }
        }
        Reciever reciever;
        private Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private String login;
    }
}