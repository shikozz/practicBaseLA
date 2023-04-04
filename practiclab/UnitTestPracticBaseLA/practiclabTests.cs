using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace practiclab.Tests
{
    [TestClass]
    public class practiclabTests
    {
        private Base.practic_LAEntities DataBase;
        [TestMethod]
        public void checkLogin()
        {
            //data
            string login = "1y3lj8w0viop@outlook.com";
            string pass = "2L6KZG";
            int expected = 1;
            DataBase = new Base.practic_LAEntities();
            //act
            LOGIN log = new LOGIN();
            log.Autho(login, pass, new Base.practic_LAEntities());
            int actual = log.userId;
            Assert.AreEqual(expected, actual);
        }
    }
}
