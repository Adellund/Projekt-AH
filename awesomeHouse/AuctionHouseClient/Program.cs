using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionHouseClient
{
    class Program
    {
        ServerHandler handler;
        bool connected;

        static void Main(string[] args)
        {
            Program myProgram = new Program();
            myProgram.Run();
        }

        private void Run()
        {
            Console.WriteLine("Write IP:");
            string ip = Console.ReadLine();
            Console.WriteLine("Write port:");
            int port = int.Parse(Console.ReadLine());

            handler = new ServerHandler(ip, port);
            string connect = handler.ConnectToServer("Andreas", "hej123", "hej123");
            Console.WriteLine(connect);

            // Checks if connected properly
            if (connect.Split(' ')[0] == "Successfully")
                connected = true;

            Thread listenThread = new Thread(ReceiveMessagesFromServer);
            listenThread.Start();

            DoDialog();
        }

        private void DoDialog()
        {
            while (connected)
            {
                string bid = Console.ReadLine();
                if (bid.ToLower() == "exit")
                    Console.WriteLine(handler.Disconnect());

                // Checks if input is in proper bid form
                if (handler.CheckBid(bid))
                    handler.SendBidToServer(bid);
                else
                    Console.WriteLine("Wrong input");
            }
        }

        private void ReceiveMessagesFromServer()
        {
            string message = handler.ReceiveMessageFromServer();

            Console.WriteLine(message);
        }
    }
}
