using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace ServerUtils
{
    abstract class Server
    {
        public Server(String IP = "127.0.0.1", int Port = 8080)
        {
            ipPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
        }
        public Server(IPAddress IP, int Port = 8080)
        {
            ipPoint = new IPEndPoint(IP, Port);
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
        public void Start(int usersCount)
        {
            mutex.WaitOne();
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(ipPoint);
                thread = new Thread(Run);
                serverCondition = true;
                socket.Listen(usersCount);
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
    abstract class NetStream
    {
        public static bool IsConnected(Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException) { return false; }
        }

        public static String RecieveMessage(Socket handler)
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
            return builder.ToString();
        }
        public static void SendMessage(Socket handler, String message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            if(handler.Connected)
            {
                handler.Send(data);
            }
        }
    }
}
