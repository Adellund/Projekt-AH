using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Threading;

namespace AuctionHouseServer
{
    class Timer
    {
        Item currentItem;
        Auctioneer auctioneer;
        // Indeholder alt timer-relateret
        public int counter = 0;
        static System.Timers.Timer aTimer;
        private int once;
        private int second;
        private int third;
        decimal lastBid;

        public Timer(Item item, Auctioneer auctioneer, int once, int second, int third)
        {
            aTimer = new System.Timers.Timer();
            currentItem = item;
            this.auctioneer = auctioneer;
            auctioneer.currentBid = item.StartPrice;
            lastBid = auctioneer.currentBid;
            this.once = once;
            this.second = second;
            this.third = third;
        }

        public void Start()
        {
            // Timer information 
            Thread.Sleep(10000);
            auctioneer.BroadcastToAllClients("Starting auction on item: " + currentItem.Name + " with start price: " + currentItem.StartPrice, false);
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;
            aTimer.Start();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (auctioneer.currentBid == lastBid)
            {
                counter++;

                if (counter == once)
                {
                    auctioneer.BroadcastToAllClients("Going Once", false);
                }
                else if (counter == second)
                {
                    auctioneer.BroadcastToAllClients("Going Twice", false);
                }
                else if (counter == third)
                {
                    auctioneer.BroadcastToAllClients("Going Third", false);
                    auctioneer.EndAuction();
                    aTimer.Enabled = false;
                    aTimer.Stop();
                    aTimer.Dispose();

                }
            }
            else
            {
                ResetTimer();
                lastBid = auctioneer.currentBid;
            }
        }

        public void ResetTimer()
        {
            counter = 0;
        }

        public void WaitForTimer()
        {
            while (aTimer.Enabled)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
