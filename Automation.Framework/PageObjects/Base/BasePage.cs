using Automation.Framework.Drivers;
using Automation.Framework.PageObjects.ElementActions;
using Automation.Framework.Waiters;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.PageObjects.Base
{
    public abstract class BasePage
    {
        protected string PageWindow;
        protected IActions Perform;
        protected WebDriverWaiter webDriverWaiter;        
        protected Guid DriverKey;
        public BasePage(Guid DriverKey)
        {
            this.DriverKey = DriverKey;
            webDriverWaiter = new WebDriverWaiter(Driver.GetDriver(DriverKey));
            Perform = new Actions(Driver.GetDriver(DriverKey),webDriverWaiter);            
        }

        public WebDriverWaiter GetWaiter()
        {
            return webDriverWaiter;
        }        

        public void SwitchWindow()
        {
            if (PageWindow == null)
            {
                PageWindow = Driver.GetDriver(DriverKey).WindowHandles[Driver.GetDriver(DriverKey).WindowHandles.Count - 1];
            }
            else
            {
                if (!Driver.GetDriver(DriverKey).WindowHandles.Contains(PageWindow))
                {
                    PageWindow = Driver.GetDriver(DriverKey).WindowHandles[Driver.GetDriver(DriverKey).WindowHandles.Count - 1];
                }
            }
            Driver.SwitchWindowTo(PageWindow, DriverKey);
        }
    }
}
