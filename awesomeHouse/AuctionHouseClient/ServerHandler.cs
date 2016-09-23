using System;
using System.IO;
using System.Net.Sockets;

namespace AuctionHouseClient
{
    class ServerHandler
    {
        StreamWriter writer;
        StreamReader reader;
        NetworkStream networkStream;
        TcpClient server;
        string serverName;
        int port;
        bool connected;
        decimal currentBid;
        string username;

        public ServerHandler(string serverName, int port)
        {
            this.serverName = serverName;
            this.port = port;
        }

        public string ConnectToServer(string username)
        {
            try
            {
                server = new TcpClient(serverName, port);
                connected = true;

                networkStream = server.GetStream();
                reader = new StreamReader(networkStream);
                writer = new StreamWriter(networkStream);


                this.username = username;
                SendLoginToServer(username);

                return "Successfully connected to IP: " + serverName;
            }
            catch (SocketException se)
            {
                return se.Message;
            }
        }

        public string Disconnect()
        {
            try
            {
                writer.Write("disconnect;{0}", username);
                connected = false;
                writer.Close();
                reader.Close();
                networkStream.Close();
                server.Close();

                serverName = string.Empty;
                port = 0;
                return "Disconnected.";
            }
            catch (SocketException se)
            {
                return se.Message;
            }
        }

        public string ReceiveMessageFromServer()
        {
            while (connected)
            {
                try
                {
                    string[] serverMessage = reader.ReadLine().Split(';');
                    switch (serverMessage[0])
                    {
                        case "bid":
                            ReceiveBid(serverMessage[1], serverMessage[2]);
                            break;
                        case "message":
                            ReceiveMessage(serverMessage[1]);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    return e.Message;
                }

            }
            return "Thread has been terminated.";
        }

        private void ReceiveBid(string bidString, string username)
        {
            decimal bid = decimal.Parse(bidString);
            currentBid = bid;
            Console.WriteLine("User {0} has bid {1}", username, bid);
        }

        private void ReceiveMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void SendBidToServer(string bid)
        {
            writer.WriteLine("bid;{0};{1}", bid, username);
            writer.Flush();
        }

        public void SendLoginToServer(string username)
        {
            writer.Write("login;{0};", username);
            writer.Flush();
        }

        public bool CheckBid(string message)
        {
            decimal bid;
            if (decimal.TryParse(message, out bid))
            {
                return true;
            }
            else if (message == "exit")
            {
                connected = false;
            }
            return false;
        }
    }
}
