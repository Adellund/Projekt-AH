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

        public string RecieveMessageFromServer()
        {
            while (connected)
            {
                try
                {
                    string serverMessage = reader.ReadLine();
                    if (serverMessage != null)
                        return serverMessage;
                }
                catch (Exception e)
                {
                    return e.Message;
                }

            }
            return "Thread has been terminated.";
        }

        public void SendMessageToServer(string message)
        {
            writer.WriteLine(message);
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
