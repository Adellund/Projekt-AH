using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseServer
{
    class Item
    {
        string Name;
        decimal StartPrice;

        public Item(string name, decimal startPrice)
        {
            this.Name = name;
            this.StartPrice = startPrice;
        }
    }
}
