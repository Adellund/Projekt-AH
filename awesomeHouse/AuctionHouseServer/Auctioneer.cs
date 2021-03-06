﻿using System;
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
        public delegate void BroadCastEventHandler(string msg, bool isBid);
        public event BroadCastEventHandler BroadCastEvent;
        public decimal currentBid;
        public string currentUser;
        List<Item> items;
        List<ClientHandler> clients;

        TcpListener tcp;
        Timer timer;
        public Auctioneer(string serverIp, int serverPort)
        {
            tcp = new TcpListener(IPAddress.Parse(serverIp), serverPort);
            tcp.Start();
            clients = new List<ClientHandler>();

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
                new Item("Stol", 20),
                new Item("Bord", 25),
                new Item("Lampe", 15)};

            foreach (Item item in items)
            {
                timer = new Timer(item, this, 10, 15, 18);
                timer.Start();
                timer.WaitForTimer();
                
            }
        }

        private void ListenForClients()
        {
            while (true)
            {
                Console.WriteLine("Listening for a connection...");
                // listening to TCP on above set ip/port
                Socket clientSocket = tcp.AcceptSocket();

                // User has connected
                Console.WriteLine("User has connected to server");
                ClientHandler handler = new ClientHandler(clientSocket, this);
                clients.Add(handler);

                // Adding the new client as a Thread
                Thread threadThis = new Thread(handler.RunThisClient);
                threadThis.Start();
            }
        }

        public void BroadcastMessage(string msg, bool isBid)
        {
            if (this.BroadCastEvent != null)
                BroadCastEvent(msg, isBid);
        }

        public void BroadcastToAllClients(string msg, bool isBid)
        {
            foreach (ClientHandler client in clients)
                client.BroadcastAction(msg, isBid);
        }

        public void EndAuction()
        {
            if (currentUser == null)
                BroadcastToAllClients("Nobody won!", false);
            else
                BroadcastToAllClients("Winner is " + currentUser, false);
        }
    }
}
