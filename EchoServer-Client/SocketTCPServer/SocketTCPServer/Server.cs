using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace SocketTCPServer
{
    abstract class Server
    {
        public Server(String IP = "127.0.0.1", int Port = 8080)
        {
            ipPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
            Start();
        }
        public Server(IPAddress IP, int Port = 8080)
        {
            ipPoint = new IPEndPoint(IP, Port);
            Start();
        }
        public void Close()
        {
            mutex.WaitOne();
            serverCondition = false;
            thread.Join();
            socket.Close();
            Console.WriteLine("Server is closed");
            mutex.ReleaseMutex();
        }
        public void Start()
        {
            mutex.WaitOne();
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(ipPoint);
                thread = new Thread(Run);
                serverCondition = true;
                thread.Start();
                Console.WriteLine("Server is running on: "+IP.ToString()+":"+Port.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            mutex.ReleaseMutex();
        }
        ~Server()
        {
            Close();
        }
        public IPAddress IP
        {
            get { return ipPoint.Address; }
            set { if(!serverCondition) { ipPoint.Address = value; } }
        }
        public int Port
        {
            get { return ipPoint.Port; }
            set { if (!serverCondition) { ipPoint.Port = value; } }
        }
        private void Run()
        {
            while (serverCondition)
            {
                mutex.WaitOne();
                if(!serverCondition)
                {
                    mutex.ReleaseMutex();
                    return;
                }
                ServerWork();
                mutex.ReleaseMutex();
                Thread.Sleep(1);
            }
        }
        protected abstract void ServerWork();

        private Mutex mutex = new Mutex();
        protected IPEndPoint ipPoint;
        protected Socket socket;
        protected bool serverCondition = false;
        protected Thread thread;
    }

    class ServerEcho : Server
    {
        public ServerEcho(String IP = "127.0.0.1", int Port = 8080) : base(IP, Port) { }
        public ServerEcho(IPAddress IP, int Port = 8080) : base(IP, Port) { }
        protected override void ServerWork()
        {
            socket.Listen(1);
            Socket handler = socket.Accept();
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
}
