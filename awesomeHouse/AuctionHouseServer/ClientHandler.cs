﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseServer
{
    class ClientHandler
    {
        private Socket clientSocket;
        private NetworkStream netStream;
        private StreamWriter writer;
        private StreamReader reader;
        private Auctioneer auctioneer;
        public string username;
        bool connected;

        public ClientHandler(Socket clientSocket, Auctioneer auctioneer)
        {
            this.clientSocket = clientSocket;
            this.auctioneer = auctioneer;
        }

        public void RunThisClient()
        {
            this.netStream = new NetworkStream(clientSocket);
            this.writer = new StreamWriter(netStream);
            this.reader = new StreamReader(netStream);
            connected = true;

            ReceiveFromClient();

            this.reader.Close();
            this.netStream.Close();
            this.clientSocket.Shutdown(SocketShutdown.Both);
            this.clientSocket.Close();
        }

        private void ReceiveFromClient()
        {
            auctioneer.BroadCastEvent += this.BroadcastAction;

            while (connected)
            {
                try
                {
                    string[] message = reader.ReadLine().Split(';');
                    switch (message[0])
                    {
                        case "login":
                            Login(message[1]);
                            break;
                        case "bid":
                            Bid(message[1]);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine(username + " has disconnected.");
                    connected = false;
                }
                
            }
        }

        public void SendToClient(string msg)
        {
            writer.WriteLine(msg);
            writer.Flush();
        }

        private void Login(string username)
        {
            this.username = username;
            Console.WriteLine(username + " has joined the server");
            auctioneer.BroadcastMessage(username + " has joined the server");
        }

        private void Bid(string bid)
        {
            try
            {
                decimal userBid = decimal.Parse(bid);
                if (userBid > auctioneer.currentBid)
                {
                    auctioneer.currentBid = decimal.Parse(bid);
                    auctioneer.currentUser = username;
                    auctioneer.BroadcastMessage("bid;" + userBid + ";" + username);
                }
                else
                {
                    SendToClient("Bid is too low");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void BroadcastAction(string msg)
        {
            SendToClient(msg);
        }
    }
}
