using Automation.Workspace.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Test
{
    [TestClass]
    public class BaseTestCase
    {
        readonly string basePage = "https://wantspeak.azurewebsites.net";

        protected Tester testerNumberOne = new Tester();

        protected Tester testerNumberTwo = new Tester();

        [TestInitialize]
        public void SetUp()
        {
            testerNumberOne.GoToPage(basePage);
            testerNumberTwo.GoToPage(basePage);
        }

        [TestCleanup]
        public void TearDown()
        {
            testerNumberOne.FinishesWork();
        }
    }
}
