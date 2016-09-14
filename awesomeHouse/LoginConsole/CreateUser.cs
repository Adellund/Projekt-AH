using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginComponent
{
    public class CreateUser : ICommand
    {
        private string email;
        private string password;
        private string confirmPassword;
        private ILoginDataMapper iLoginDataMapper;

        public CreateUser(string email, string password, string confirmPassword, ILoginDataMapper iLoginDataMapper)
        {
            this.email = email;
            this.password = password;
            this.confirmPassword = confirmPassword;
            this.iLoginDataMapper = iLoginDataMapper;
        }
        public void Execute()
        {
            try
            {
                Utilities.CheckEmail(email);
                Utilities.CheckPassword(password, confirmPassword);

                string hashedPassword = Utilities.HashPassword(password);
                User user = new User(email, hashedPassword);
                iLoginDataMapper.Create(user);
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
    }
}
