using System;
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

        public ClientHandler(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
        }

        public void RunThisClient()
        {
            this.netStream = new NetworkStream(clientSocket);
            this.writer = new StreamWriter(netStream);
            this.reader = new StreamReader(netStream);

            DoDialog();

            this.reader.Close();
            this.netStream.Close();
            this.clientSocket.Shutdown(SocketShutdown.Both);
            this.clientSocket.Close();
        }

        private void DoDialog()
        {
            writer.WriteLine();
            writer.Flush();
        }

        private void ReceiveFromServer()
        {

        }
    }
}
