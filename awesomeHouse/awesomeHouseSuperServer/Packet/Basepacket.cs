using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace awesomeHouseSuperServer
{
    public class BasePacket
    {
        //Formatter formatter = new Formatter();


        public void Serialize()
        {
            //byte[] data;
            
            using (var bit = new MemoryStream())
            {
                
            }

        }

        public void Deserialize()
        {

        }
    }
}
