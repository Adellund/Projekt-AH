using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AuctionHouseServer
{
    class Auctioneer
    {
        public delegate void BroadCastEventHandler(string msg);
        public event BroadCastEventHandler BroadCastEvent;

        TcpListener tcp;
        Timer timer;
        public Auctioneer(string serverIp, int serverPort)
        {
            tcp = new TcpListener(IPAddress.Parse(serverIp), serverPort);
            tcp.Start();
            timer = new Timer(10, 5, 3);
        }

        private void Run()
        {
            // Thread for listening for clients
            Thread listenForClientsThread = new Thread(ListenForClients);
            listenForClientsThread.Start();

            StartAuction();
            BroadcastStatus();
        }

        private void StartAuction()
        {
            timer.Start();
        }

        private void ListenForClients()
        {
            while (true)
            {
                // listening to TCP on above set ip/port
                Socket clientSocket = tcp.AcceptSocket();

                // User has connected
                Console.WriteLine("User has connected to server");
                ClientHandler handler = new ClientHandler(clientSocket);

                // Adding the new client as a Thread
                Thread threadThis = new Thread(handler.RunThisClient);
                threadThis.Start();
            }
        }


        private void BroadcastStatus()
        {

        }

        public void BroadcastMessage(string msg)
        {
            //sendToClient("Broadcast:" + msg);
        }
    }
}
