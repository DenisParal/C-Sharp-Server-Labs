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
        protected Socket handler;
        protected IPEndPoint ipPoint;
        protected Socket socket;
        protected bool serverCondition = false;
        protected Thread thread;
    }
}
