using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using ServerData;

namespace Server
{
    class Server
    {
        static Socket listenerSocket; 
        static Thread listenerThread;
        static List<clientData> _clients;
        static void Main(string[] args)
        {
            Console.WriteLine("Server start");
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
            listenerSocket.Bind(endPoint);
            Thread listenThread = new Thread(ListenThread);
            listenThread.Start();
            Console.WriteLine("Success... Listening IP: " + Packet.GetIP4Adress() + ":8888");
        }
        static void ListenThread()
        {
            while (true)
            {
                listenerSocket.Listen(0);
                _clients.Add(new clientData(listenerSocket.Accept()));
            }
        }
        public static void Data_IN(object cSocket)
        {
            Socket clientSocket = (Socket)cSocket;

            byte[] Buffer;
            int readBytes;

            while (true)
            {
                try
                {
                    Buffer = new byte[clientSocket.SendBufferSize];
                    readBytes = clientSocket.Receive(Buffer);
                    if (readBytes > 0)
                    {
                        Packet p = new Packet(Buffer);
                        DataManager(p);
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("Client Disconnected.");
                }
            }
        }
        public static void DataManager(Packet p)
        {
            switch (p.packetType)
            {
                case PacketType.Chat:
                    SendMessageToCLients(p);
                    break;


                case PacketType.CloseConnection:
                    var exitClient = GetClientByID(p);

                    CloseClientConnection(exitClient);
                    RemoveClientFromList(exitClient);
                    SendMessageToCLients(p);
                    AbortClientThread(exitClient);
                    break;
            }
        }

        /// <summary>
        /// send message for each client in clietn list
        /// </summary>
        /// <param name="p"></param>
        public static void SendMessageToCLients(Packet p)
        {
            foreach (clientData c in _clients)
            {
                c.clientSocket.Send(p.ToBytes());
            }
        }

        /// <summary>
        /// get specific client by id
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private static clientData GetClientByID(Packet p)
        {
            return (from client in _clients
                    where client.id == p.senderID
                    select client)
                    .FirstOrDefault();

        }

        private static void CloseClientConnection(clientData c)
        {
            c.clientSocket.Close();
        }
        private static void RemoveClientFromList(clientData c)
        {
            _clients.Remove(c);
        }
        private static void AbortClientThread(clientData c)
        {
            c.clientThread.Abort();
        }
    }
}
