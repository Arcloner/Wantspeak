using Automation.AOP;
using Automation.Framework.Drivers;
using Automation.Workspace.IoC;
using Automation.Workspace.Steps;
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

        private DependencyResolver resolver;

        public Tester()
        {
            resolver = new DependencyResolver();
            resolver.Register(new PageObjectsRegistration());
        }

        public StartPageSteps AtStartPage()
        {
            return new StartPageSteps(driver.GetDriverKey(),resolver);
        }

        public ResultsOfSearchSteps AtResultsOfSearchPage()
        {
            return new ResultsOfSearchSteps(driver.GetDriverKey(),resolver); 
        }

        public RegisterPageSteps AtRegisterPage()
        {
            return new RegisterPageSteps(driver.GetDriverKey(),resolver);
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
