using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace awesomeHouseSuperServer
{
    public class RequestHandler
    {
        public string CreateUser (string username, string password)
        {
            
            User myUser = new User(username, password);

            ListClass.userList.Add(myUser);

            return "User Created";
        }
    }
}
