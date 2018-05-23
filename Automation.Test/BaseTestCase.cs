using Automation.Workspace.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Test
{
    [TestClass]
    public class BaseTestCase
    {
        readonly string basePage = "https://www.kinopoisk.ru/";

        protected Tester tester = new Tester();

        [TestInitialize]
        public void SetUp()
        {
            tester.GoToPage(basePage);
        }

        [TestCleanup]
        public void TearDown()
        {
            tester.FinishesWork();
        }
    }
}
