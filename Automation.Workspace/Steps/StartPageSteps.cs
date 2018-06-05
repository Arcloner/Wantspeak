using Automation.AOP;
using Automation.Workspace.PageObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Workspace.Steps
{
    public class StartPageSteps
    {
        IStartPage startPage;

        public StartPageSteps(Guid driverId,DependencyResolver resolver)
        {
            startPage = resolver.For<IStartPage>(driverId);
        }

        public void SearchFilm(string filmName)
        {
            startPage.SendKeysToSearchInput(filmName);
            startPage.ClickSearchButton();
        }
    }
}
