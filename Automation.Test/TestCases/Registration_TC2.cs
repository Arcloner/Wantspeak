using Automation.Test;
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
    public class Registration_TC2: BaseTestCase
    {
        [TestMethod]
        public void RegisterUser()
        {
            tester.AtRegisterPage().RegisterUser("Pinval", "21", "Mogilev");
        }
    }
}
