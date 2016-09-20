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
    public class Auctioneer
    {
        public delegate void BroadCastEventHandler(string msg);
        public event BroadCastEventHandler BroadCastEvent;
        public decimal currentBid;
        List<Item> items;

        TcpListener tcp;
        Timer timer;
        public Auctioneer(string serverIp, int serverPort)
        {
            tcp = new TcpListener(IPAddress.Parse(serverIp), serverPort);
            tcp.Start();

            Thread listenForClientsThread = new Thread(ListenForClients);
            listenForClientsThread.Start();

            Run();
        }

        private void Run()
        {
            StartAuction();
        }

        private void StartAuction()
        {
            items = new List<Item> {
                new Item("Kød", 20),
                new Item("Kim", 5),
                new Item("Pølser", 10)};

            foreach (Item item in items)
            {
                timer = new Timer(this, 10, 15, 18);
                timer.Start();
            }
        }

        private void ListenForClients()
        {
            while (true)
            {
                Console.WriteLine("listening");
                // listening to TCP on above set ip/port
                Socket clientSocket = tcp.AcceptSocket();

                // User has connected
                Console.WriteLine("User has connected to server");
                ClientHandler handler = new ClientHandler(clientSocket, this);

                // Adding the new client as a Thread
                Thread threadThis = new Thread(handler.RunThisClient);
                threadThis.Start();
            }
        }

        public void BroadcastMessage(string msg)
        {
            if (this.BroadCastEvent != null)
                BroadCastEvent(msg);
        }

    }
}
