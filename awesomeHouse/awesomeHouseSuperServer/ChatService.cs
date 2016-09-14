using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace awesomeHouseSuperServer
{
    public class ChatService
    {
        public delegate void BroardCastEventHandler(string msg);  

        public event BroardCastEventHandler BroardCastEvent;      
        private string name;

        public ChatService(string name)
        {
            this.name = name;
        }
        public string Name
        {
            get { return this.name; }
        }

        public void BroadCastBesked(string msg)
        {
            if (this.BroardCastEvent != null)  
                BroardCastEvent(msg);          
        }
    }
}
