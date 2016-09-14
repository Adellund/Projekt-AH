using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace awesomeHouseTestProject
{
    [TestClass]
    public class ClientConnection
    {
        [TestMethod]
        public void TestIP()
        {
            string ip = "127.0.0.1";
            int port = 1234;
            bool success = false;
            FakeServerConnection fakeConnection = new FakeServerConnection(ip, port);
            success = fakeConnection.Connect();
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void TestIpFailure()
        {
            string ip = "127.0.0.2";
            int port = 1234;
            string message;
            FakeServerConnection fakeConnection = new FakeServerConnection(ip, port);
            try
            {
                fakeConnection.Connect();
                message = "success";
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            Assert.AreEqual(message, "Ups, Wrong Ip");

        }

        [TestMethod]
        public void TestPort()
        {
            string ip = "127.0.0.1";
            int port = 1234;
            bool success = false;
            FakeServerConnection fakeConnection = new FakeServerConnection(ip, port);
            success = fakeConnection.Connect();
            Assert.IsTrue(success);

        }
        [TestMethod]
        public void TestPortFailure()
        {
            string ip = "127.0.0.1";
            int port = 1235;
            string message;
            FakeServerConnection fakeConnection = new FakeServerConnection(ip, port);
            try
            {
                fakeConnection.Connect();
                message = "success";
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            
            Assert.AreEqual(message, "Ups, Wrong Port");

        }
    }
}
