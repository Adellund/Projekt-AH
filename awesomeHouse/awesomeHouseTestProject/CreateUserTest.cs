using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoginModuleTest
{
    [TestClass]
    public class CreateUserTest
    {
        [TestMethod]
        public void CreateUser_AllInputOk_Exception()
        {
            bool success = false;
            try
            {
                LoginComponent.ILoginDataMapper fakeLogin = new FakeLoginDataMapper();
                LoginComponent.Login login = new LoginComponent.Login(fakeLogin);
                login.Create("FiskeFrederik@gmail.com", "123456Aa", "123456Aa");
                success = true;
            }
            catch (Exception)
            {
            }
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void CreateUser_TooShortPassword_Exception()
        {
            bool failed = false;
            try
            {
                LoginComponent.ILoginDataMapper fakeLogin = new FakeLoginDataMapper();
                LoginComponent.Login login = new LoginComponent.Login(fakeLogin);
                login.Create("FiskeFrederik@gmail.com", "123Aa", "123Aa");
            }
            catch (Exception e)
            {
                if (e.Message == "Password too short")
                {
                    failed = true;
                }
            }
            Assert.IsTrue(failed);
        }

        [TestMethod]
        public void CreateUser_TooLongPassword_Exception()
        {
            bool failed = false;
            try
            {
                LoginComponent.ILoginDataMapper fakeLogin = new FakeLoginDataMapper();
                LoginComponent.Login login = new LoginComponent.Login(fakeLogin);
                login.Create("FiskeFrederik@gmail.com", "1234567891011121314151617181920Aa", "1234567891011121314151617181920Aa");
            }
            catch (Exception e)
            {
                if (e.Message == "Password too long")
                {
                    failed = true;
                }
            }
            Assert.IsTrue(failed);
        }

        [TestMethod]
        public void CreateUser_NoSmallCharactersInPassword_Exception()
        {
            bool failed = false;
            try
            {
                LoginComponent.ILoginDataMapper fakeLogin = new FakeLoginDataMapper();
                LoginComponent.Login login = new LoginComponent.Login(fakeLogin);
                login.Create("FiskeFrederik@gmail.com", "123456A", "123456A");
            }
            catch (Exception e)
            {
                if (e.Message == "Password needs at least one small letter, one capitalized letter and one number")
                {
                    failed = true;
                }
            }
            Assert.IsTrue(failed);
        }

        [TestMethod]
        public void CreateUser_NoCapitalizedCharactersInPassword_Exception()
        {
            bool failed = false;
            try
            {
                LoginComponent.ILoginDataMapper fakeLogin = new FakeLoginDataMapper();
                LoginComponent.Login login = new LoginComponent.Login(fakeLogin);
                login.Create("FiskeFrederik@gmail.com", "123456a", "123456a");
            }
            catch (Exception e)
            {
                if (e.Message == "Password needs at least one small letter, one capitalized letter and one number")
                {
                    failed = true;
                }
            }
            Assert.IsTrue(failed);
        }

        [TestMethod]
        public void CreateUser_NoNumbersInPassword_Exception()
        {
            bool failed = false;
            try
            {
                LoginComponent.ILoginDataMapper fakeLogin = new FakeLoginDataMapper();
                LoginComponent.Login login = new LoginComponent.Login(fakeLogin);
                login.Create("FiskeFrederik@gmail.com", "Abcdefg", "Abcdefg");
            }
            catch (Exception e)
            {
                if (e.Message == "Password needs at least one small letter, one capitalized letter and one number")
                {
                    failed = true;
                }
            }
            Assert.IsTrue(failed);
        }

        [TestMethod]
        public void CreateUser_NotIdenticalPasswords_Exception()
        {
            bool failed = false;
            try
            {
                LoginComponent.ILoginDataMapper fakeLogin = new FakeLoginDataMapper();
                LoginComponent.Login login = new LoginComponent.Login(fakeLogin);
                login.Create("FiskeFrederik@gmail.com", "123456Aa!", "1234567Aa!");
            }
            catch (Exception e)
            {
                if (e.Message == "Passwords are not identical")
                {
                    failed = true;
                }
            }
            Assert.IsTrue(failed);
        }

        [TestMethod]
        public void CreateUser_InvalidCharacterInEmail_Exception()
        {
            bool failed = false;
            try
            {
                LoginComponent.ILoginDataMapper fakeLogin = new FakeLoginDataMapper();
                LoginComponent.Login login = new LoginComponent.Login(fakeLogin);
                login.Create("#hashtag@gmail.com", "123456Aa", "123456Aa");
            }
            catch (Exception e)
            {
                if (e.Message == "Invalid characters in the email")
                {
                    failed = true;
                }
            }
            Assert.IsTrue(failed);
        }

        [TestMethod]
        public void CreateUser_MissingAtSymbolInEmail_Exception()
        {
            bool failed = false;
            try
            {
                LoginComponent.ILoginDataMapper fakeLogin = new FakeLoginDataMapper();
                LoginComponent.Login login = new LoginComponent.Login(fakeLogin);
                login.Create("hashtag.com", "123456Aa", "123456Aa");
            }
            catch (Exception e)
            {
                if (e.Message == "Invalid email format")
                {
                    failed = true;
                }
            }
            Assert.IsTrue(failed);
        }
    }
}
