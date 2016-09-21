using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseServer
{
    class Item
    {
        string name;
        decimal startPrice;

        public Item(string name, decimal startPrice)
        {
            this.name = name;
            this.startPrice = startPrice;
        }
        public string Name
        {
            get
            {
                return name;
            }
        }

        public decimal StartPrice
        {
            get
            {
                return startPrice;
            }
        }
    }
}
