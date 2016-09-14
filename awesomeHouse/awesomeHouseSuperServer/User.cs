using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace awesomeHouseSuperServer
{
    [Serializable]
    public class User
    {
        public string Username { get; }
        public string Password { get; }

        //private string username;
        //private string password;
        public User()
        {

        }
        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
