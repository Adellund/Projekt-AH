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
        private ChatService chatService;

        // Constructor der opretter Clienthandleren til Chatservicen
        public ClientHandler(Socket clientSocket, ChatService chatService)
        {
            this.clientSocket = clientSocket;
            this.chatService = chatService;
        }

        // Runs the threadded client when called
        public void RunThisClient()
        {
            // Instantiering af streamwriter, reader og en netværksstream der knyttes til klassens private variable.
            this.netStream = new NetworkStream(clientSocket);
            this.writer = new StreamWriter(netStream);
            this.reader = new StreamReader(netStream);

            doDialog();

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

        private string receiveFromClient()
        {
            try
            {
                test();
                return reader.ReadLine();
            }
            catch
            {
                return null;
            }
            
        }

        public void test()
        {
            User user = new User("tobias", "123lololol");
            byte[] data = Serialize(user);
            User user2 = new User();
            user2 = DeSerialize(data);
            // PSEUDO PSEUDO KODE - Benny, lol.
            //bool b = false;

            //if (et eller andet == "jens")
            //{
            //    b = true;
            //}
            //else false;

            //AssertIf(b)
            //    user in = new(navn, passsord);
            //user out = deserie(serialisering(in));
            //asser - areequal(in.name == out.name);
        }

        public byte[] Serialize (User user)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, user);
            return ms.ToArray();
        }
        public User DeSerialize(byte[] data)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            ms.Write(data, 0, data.Length);
            ms.Seek(0, SeekOrigin.Begin);
            object o = (object)bf.Deserialize(ms);
            return (User)o;
        }


        private void doDialog()
        {
            try
            {
                sendToClient("Server ready - write EXIT to exit the program");
                chatService.BroardCastEvent += this.BroadcastAction;

                while (executeCommand() == true)
                {

                }                          
            }
            catch
            {

            }
            finally
            {
                chatService.BroardCastEvent -= this.BroadcastAction;      
            }
        }

        private bool executeCommand()  
        {
            string cmd = receiveFromClient();
            bool run = true;
            while (run)
            {
                switch (cmd.ToLower())
                {
                    case "create user":
                        CreateUser();
                        break;
                    default:
                        break;
                }
            }
            if (cmd == null)
            {
                return false;
            }
            //if (input.Trim().ToLower() == "exit")
            //{
            //    return false;
            //}
            
            //chatService.BroadCastBesked(input);

            return true;
        }

        public void BroadcastAction(string msg)
        {
            sendToClient("Broadcast:" + msg);
        }
        public List<User> CreateUser()
        {
            return new List<User>();
        }
    }
}
