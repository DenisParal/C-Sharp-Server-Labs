using System;
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
        class Receiver
        {
            public Receiver(Socket socket)
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
                while (ServerUtils.NetStream.IsConnected(socket))
                {
                    data = new byte[256];
                    builder = new StringBuilder();
                    int bytes = 0;
                    try
                    {
                        do
                        {
                            if (!ServerUtils.NetStream.IsConnected(socket))
                            {
                                break;
                            }
                            bytes = socket.Receive(data, data.Length, 0);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        while (socket.Available > 0);
                    }
                    catch(SocketException)
                    {
                        break;
                    }
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
            ~Receiver()
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
            receiver = new Receiver(socket);
        }
        public void Disconnect()
        {
            if(Connected)
            {
                SendMessage("/Exit");
                socket.Disconnect(true);
                receiver.Stop();
            }
        }
        public String RecieveMessage()
        {
            List<String> data = receiver.Get();
            String result = "";
            foreach (var line in data)
            {
                result += line + "\n";
            }
            return result;
        }
        public void SendMessage(String message)
        {
            if(socket.Connected)
            {
                byte[] sentData = Encoding.Unicode.GetBytes(message);
                socket.Send(sentData);
            }
        }
        public bool Connected
        {
            get { return socket.Connected; }
        }
        Receiver receiver;
        private Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private String login;
    }
}
