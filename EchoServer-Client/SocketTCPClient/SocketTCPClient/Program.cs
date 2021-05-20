using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketTCPClient
{
    class Program
    {
        static int port = 8080;
        static string address = "127.0.0.1";
        public static void sendMeta(Socket socket)
        {
            if(socket.Connected)
            {
                String user = Dns.GetHostName();
                String user_ip = Dns.GetHostEntry(user).AddressList[1].ToString();
                Console.WriteLine(user_ip);
                String meta = user + " - " + user_ip;
                byte[] data = Encoding.Unicode.GetBytes(meta);
                socket.Send(data);
            }
        }
        static void Main(string[] args)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                sendMeta(socket);

                byte[] data = new byte[256];
                StringBuilder builder = new StringBuilder();
                int bytes = 0;

                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                Console.WriteLine("Ответ сервера: " + builder.ToString());

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
