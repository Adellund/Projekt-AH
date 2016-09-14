using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace awesomeHouseSuperServer
{
    public class Server
    {
        public Server (string serverIp, int serverPort)
        {
            // Server startes med info sendt til Constructor :
            Console.Clear();
            Console.WriteLine("starting server on ...");
            Console.WriteLine("Ip: " + serverIp);
            Console.WriteLine("Port " + serverPort);

            // Starting the TCP listener
            TcpListener tcp = new TcpListener(IPAddress.Parse(serverIp), serverPort);
            tcp.Start();

            while (true)
            {
                // listening to TCP on above set ip/port
                Console.WriteLine("Server is running..");
                Socket clientSocket = tcp.AcceptSocket();

                // User has connected
                Console.WriteLine("User has connected to server");
                ClientHandler handler = new ClientHandler(clientSocket);

                // Adding the new client as a Thread
                Thread threadThis = new Thread(handler.RunThisClient);
                threadThis.Start();
            }
        }
    }
}
