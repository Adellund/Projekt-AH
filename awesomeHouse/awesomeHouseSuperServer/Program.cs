using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace awesomeHouseSuperServer
{
    public class Program
    {
        
        static void Main(string[] args)
        {

            // SET THE IP
            Console.WriteLine("Choose an ip");
            Console.WriteLine("if left empty default will be set to "+ GetLocalIPAddress() + " :");
            string serverStartupIp = Console.ReadLine();
            if (serverStartupIp.Trim() == "")
            {
                serverStartupIp = GetLocalIPAddress();
            }

            // SET THE PORT
            Console.WriteLine("Choose an port");
            Console.WriteLine("if left empty default will be set to 666 : ");
            string serverStartupPort = Console.ReadLine();
            if (serverStartupPort.Trim() == "")
            {
                serverStartupPort = "666";
            }
            int serverStartupPortParsed = int.Parse(serverStartupPort);

            // Start constructor "Server" on IP, PORT
            new Server(serverStartupIp, serverStartupPortParsed);
        }

        // GET LOCAL IP
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}
