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
        static List<ClientData> _clients;

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
                        Packet packet = new Packet(buffer);
                        DataManager(packet);
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("Client Disconnected");
                }
            }
        }

        public static void DataManager(Packet p)
        {
            switch (p.packetType)
            {
                case PacketType.Chat:
                    break;
                case PacketType.CloseConnection:
                    break;

            }
        }

        public static void SendMessageToClients(Packet p)
        {
            foreach (ClientData c in _clients)
            {
                c.clientSocket.Send(p.ToBytes());
            }
        }

        private static ClientData GetClientById(Packet p)
        {
            return (from client in _clients
                    where client.id == p.senderID
                    select client)
        }
    }
}
