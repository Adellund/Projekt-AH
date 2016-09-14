using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml;


namespace awesomeHouseSuperServer
{
    public class ClientHandler
    {
        // nu tilgængelig påtværs af klasser
        private Socket clientSocket;
        private NetworkStream netStream;
        private StreamWriter writer;
        private StreamReader reader;

        // Constructor der opretter Clienthandleren til Chatservicen
        public ClientHandler(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
        }

        // Runs the threadded client when called
        public void RunThisClient()
        {
            // Instantiering af streamwriter, reader og en netværksstream der knyttes til klassens private variable.
            this.netStream = new NetworkStream(clientSocket);
            this.writer = new StreamWriter(netStream);
            this.reader = new StreamReader(netStream);

            receiveFromClient(reader.ReadLine());

            this.writer.Close();
            this.reader.Close();
            this.netStream.Close();
            this.clientSocket.Shutdown(SocketShutdown.Both);
            this.clientSocket.Close();

        }
        private void sendToClient(string text)
        {
            writer.WriteLine(text);
            writer.Flush();
        }

        private void receiveFromClient(byte[] data)
        {
            
            try
            {
                RequestHandler requestHandler = new RequestHandler();
                bool cmd = true;
                object receivedObject = DTO.DeserializeObject.Deserialize(data);
                string type = receivedObject.GetType().ToString();
                while (cmd == true)
                {
                    switch (type)
                    {
                        case "CreateUser":
                            requestHandler.CreateUser(receivedObject);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch
            {

            }

        }

        public void BroadcastAction(string msg)
        {
            sendToClient("Broadcast:" + msg);
        }
    }
}
