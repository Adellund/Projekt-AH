using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace awesomeHouseTestProject
{
    class FakeServerConnection
    {
        private string Ip;
        private int Port;

       public FakeServerConnection(string ip, int port)
        {
            this.Ip = ip;
            this.Port = port;
        }

        public bool Connect()
        {
            if (Ip == "127.0.0.1")
            {

                if (Port == 1234)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Ups, Wrong Port");
                }
            }
            else
            {
                throw new Exception("Ups, Wrong Ip");
            }
        }

    }
}
