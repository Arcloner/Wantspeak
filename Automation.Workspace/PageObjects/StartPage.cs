using Automation.Framework.Drivers;
using Automation.Framework.PageObjects.Base;
using Automation.Workspace.PageObjects.Interfaces;
using OpenQA.Selenium;
using System;

namespace Automation.Workspace.PageObjects
{
    public class StartPage : BasePage, IStartPage
    {
        public StartPage(Guid DriverKey) : base(DriverKey){}        

        private IWebElement searchButton => Driver.GetDriver(DriverKey).FindElementById("btnSearch");        

        public void ClickSearchButton()
        {
            Perform.HowerThenClick(searchButton);
        }        
    }
}
