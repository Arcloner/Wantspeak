using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.Waiters
{
    public class WebDriverWaiter
    {
        IWebDriver driver;

        public WebDriverWaiter(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void WaitForElementIsClickable(IWebElement element)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }
        public void WaitForDOMLoad()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            wait.Until(wd => (string)js.ExecuteScript("return document.readyState") == "complete");
        }
        public void WaitForElementIsStalenessOf(IWebElement element)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.StalenessOf(element));
        }
        public void Wait(int SecondsToWait)
        {
            var now = DateTime.Now;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(SecondsToWait));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Until(wd => (DateTime.Now - now) - TimeSpan.FromSeconds(SecondsToWait) > TimeSpan.Zero);
        }
        public void WaitForAjaxLoad()
        {
            if (jQueryExists())
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                wait.Until(wd => (bool)(driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0"));
            }
        }
        bool jQueryExists()
        {
            try
            {
                (driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
