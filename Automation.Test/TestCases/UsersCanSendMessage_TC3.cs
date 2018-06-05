using Automation.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Test.TestCases
{
    [TestClass]
    public class UsersCanSendMessage_TC3:TwoTestersBaseTestCase
    {
        [TestInitialize]
        public void BeforeTest()
        {
            testerNumberOne.AtRegisterPage().RegisterUser("User1", "21", "Mogilev");
            testerNumberTwo.AtRegisterPage().RegisterUser("User2", "21", "Mogilev");
        }

        [TestMethod]
        public void UsersCanSendMessage()
        {
            testerNumberOne.AtStartPage().StartSearch();            
            testerNumberTwo.AtStartPage().StartSearch();
            testerNumberOne.AtChatPage().SendMessage("Hello guy , how are you?");
            testerNumberTwo.AtChatPage().SendMessage("Hi, i am fine thank you");
        }
    }
}
