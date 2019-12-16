using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using ServerData;

namespace Server
{
    class ClientData
 
    {
        public Socket clientSocket;
        public Thread clientThread;
        public string id;

        public ClientData()
        {
            id = new Guid().ToString();
            clientThread = new Thread(Server.DataIn);
            clientThread.Start(clientSocket);
            SendRegistrationPacketToClient();
        }

        public ClientData(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            id = Guid.NewGuid().ToString();
            clientThread = new Thread(Server.DataIn);
            clientThread.Start(clientSocket);
            SendRegistrationPacketToClient();
        }

        public void SendRegistrationPacketToClient()
        {
            Packet p = new Packet(PacketType.Registration, "server");
            p.data.Add(id);
            clientSocket.Send(p.ToBytes());
        }

    }
}
