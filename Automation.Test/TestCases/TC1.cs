using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Test.TestCases
{
    [TestClass]
    public class TC1:BaseTestCase
    {
        [TestInitialize]
        public void BeforeTest()
        {
            testerNumberOne.AtRegisterPage().RegisterUser("User1", "21", "Mogilev");
            testerNumberTwo.AtRegisterPage().RegisterUser("User2", "21", "Mogilev");
        }
        [TestMethod]
        public void UsersFindEachOthers()
        {
            
        }
    }
}
