using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using ServerData;

namespace Server
{
    class Server
    {
        static Socket listenerSocket;
        
        static void Main(string[] args)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerSocket.Bind(endPoint);
            Thread thread = new Thread(ListeningThread);
        }

        static void ListeningThread()
        {

        }

        public static void DataIn(Object cSocket)
        {
            Socket clientSocket = (Socket)cSocket;
            byte[] buffer;
            int readBytes;
            while(true)
            {
                try
                {
                    buffer = new byte[clientSocket.SendBufferSize];
                    readBytes = clientSocket.Receive(buffer);
                    if (readBytes > 0)
                    {

                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("Client Disconnected");
                }
            }
        }
    }
}
