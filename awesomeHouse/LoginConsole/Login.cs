namespace LoginComponent
{
    public class Login
    {
        private ILoginDataMapper loginDataMapper;
        public Login(ILoginDataMapper ldm)
        {
            loginDataMapper = ldm;
        }

        public void Create(string email, string password, string confirmPassword)
        {
            try
            {
                new CreateUser(email, password, confirmPassword, loginDataMapper).Execute();
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}