using Automation.Framework.Drivers;
using Automation.Framework.PageObjects.Base;
using Automation.Workspace.PageObjects.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Workspace.PageObjects
{
    public class ResultsOfSearch : BasePage, IResultsOfSearch
    {
        public ResultsOfSearch(Guid DriverKey) : base(DriverKey){}

        private IWebElement resultOfSearch => Driver.GetDriver(DriverKey).FindElementByXPath("//div[contains(@class,'results')]//div[contains(@class,'most_wanted')]//div[contains(@class,'info')]//p/a");

        public void ClickSearchedFilm()
        {
            resultOfSearch.Click();
        }

        public string GetTextOfSearchedFilm()
        {
            return resultOfSearch.Text;
        }
    }
}
