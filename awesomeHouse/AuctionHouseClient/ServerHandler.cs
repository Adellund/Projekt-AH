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

        public string ConnectToServer(string username, string password, string confirmPassword)
        {
            try
            {
                server = new TcpClient(serverName, port);
                connected = true;

                networkStream = server.GetStream();
                reader = new StreamReader(networkStream);
                writer = new StreamWriter(networkStream);

                this.username = username;
                writer.Write("login;{0};{1};{2}", username, password, confirmPassword);
                writer.Flush();

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
                    string serverMessage = reader.ReadLine();
                    if (serverMessage != null)
                    {
                        string[] message = serverMessage.Split(';');
                        if (message[0].ToLower() == "bid")
                        {
                            currentBid = decimal.Parse(message[1]);
                            return "New highest bid from user " + message[2] + ": " + message[1];
                        }
                        return serverMessage;
                    }
                }
                catch (Exception e)
                {
                    return e.Message;
                }

            }
            return "Thread has been terminated.";
        }

        public void SendBidToServer(string bid)
        {
            writer.WriteLine("bid;{0};{1}", bid, username);
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
