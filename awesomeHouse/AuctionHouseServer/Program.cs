using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Auctioneer auctioneer = new Auctioneer("127.0.0.1", 666);

            Auctioneer auctioneer = new Auctioneer("10.140.109.178", 666);
        }
    }
}
