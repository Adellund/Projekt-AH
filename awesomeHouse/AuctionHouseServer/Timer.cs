using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace AuctionHouseServer
{
    class Timer
    {
        // Indeholder alt timer-relateret
        public int counter = 0;
        static System.Timers.Timer aTimer = new System.Timers.Timer();
        private int once;
        private int second;
        private int third;

        public Timer(int once, int second, int third)
        {
            this.once = once;
            this.second = second;
            this.third = third;
        }

        // Fiks Console.WriteLine()s i class, så der kan blive foretaget ordentlige tests.
        public void Start()
        {
            // Timer information 
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;
            aTimer.Start();
            Console.WriteLine(counter.ToString());
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            counter--;

            // Show or hide counter
            Console.WriteLine(counter.ToString());

            if (counter == once)
            {
                Console.WriteLine("Going Once");
            }
            else if (counter == second)
            {
                Console.WriteLine("Going Twice");
            }
            else if (counter == third)
            {
                aTimer.Stop();
                Console.WriteLine("Going Third");
                Console.WriteLine("Winner");
            }
        }

        public void ResetTimer()
        {

        }
    }
}
