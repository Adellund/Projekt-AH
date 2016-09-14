using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginComponent
{
    public class User
    {
        public string Email { get; set; }
        public string HashedPassword { get; set; }

        public User(string email, string hashedPassword)
        {
            Email = email;
            HashedPassword = hashedPassword;
        }
    }
}
