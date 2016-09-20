using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHousePackets
{
    public class LoginPackage
    {
        public static string Login(string username, string password, string confirmPassword)
        {
            return "login;" + username + ";" + password + ";" + confirmPassword;
        }
    }
}
