using Automation.AOP;
using Automation.Framework.Drivers;
using Automation.Workspace.IoC;
using Automation.Workspace.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Workspace.Users
{
    public class Tester
    {
        private Driver driver = new Driver(WebBrowsers.Chrome);

        public Tester()
        {
            DependencyResolver.Register(new PageObjectsRegistration());
        }

        public StartPageSteps AtStartPage()
        {
            return new StartPageSteps(driver.GetDriverKey());
        }

        public ResultsOfSearchSteps AtResultsOfSearchPage()
        {
            return new ResultsOfSearchSteps(driver.GetDriverKey()); 
        }

        public void GoToPage(string url)
        {
            driver.GoToUrl(url);
        }

        public void FinishesWork()
        {
            driver.CloseDriver();
        }
    }
}
