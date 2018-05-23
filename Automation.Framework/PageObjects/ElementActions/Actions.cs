using Automation.Framework.Waiters;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.PageObjects.ElementActions
{
    public class Actions : IActions
    {
        DefaultWaiter defaultWaiter = new DefaultWaiter();

        IWebDriver driver;

        WebDriverWaiter webDriverWaiter;

        public Actions(IWebDriver driver,WebDriverWaiter webDriverWaiter)
        {
            this.driver = driver;
            this.webDriverWaiter = webDriverWaiter;
            webDriverWaiter = new WebDriverWaiter(driver);
        }

        public void Click(IWebElement element)
        {
            defaultWaiter.WaitForElementIsDisplayed(element);
            defaultWaiter.WaitForElementIsEnabled(element);
            defaultWaiter.WaitForElementIsVisible(element);
            webDriverWaiter.WaitForElementIsClickable(element);
            element.Click();
        }

        public void Click(IWebElement element, int delay)
        {
            defaultWaiter.WaitForElementIsDisplayed(element);
            defaultWaiter.WaitForElementIsEnabled(element);
            defaultWaiter.WaitForElementIsVisible(element);
            webDriverWaiter.WaitForElementIsClickable(element);
            webDriverWaiter.Wait(delay);
            element.Click();
        }

        public void HowerElement(IWebElement element)
        {
            new OpenQA.Selenium.Interactions.Actions(driver).MoveToElement(element).Perform();
            webDriverWaiter.WaitForDOMLoad();
            webDriverWaiter.WaitForAjaxLoad();
        }

        public void HowerThenClick(IWebElement element)
        {
            HowerElement(element);
            Click(element);
        }

        public void SendKeys(IWebElement element, string keys)
        {
            defaultWaiter.WaitForElementIsDisplayed(element);
            defaultWaiter.WaitForElementIsEnabled(element);
            defaultWaiter.WaitForElementIsVisible(element);
            webDriverWaiter.WaitForElementIsClickable(element);
            element.SendKeys(keys);
        }

        public void SendKeys(IWebElement element, string keys, int delay)
        {
            defaultWaiter.WaitForElementIsDisplayed(element);
            defaultWaiter.WaitForElementIsEnabled(element);
            defaultWaiter.WaitForElementIsVisible(element);
            webDriverWaiter.WaitForElementIsClickable(element);
            webDriverWaiter.Wait(delay);
            element.SendKeys(keys);
        }

        public void UploadFile(IWebElement element, string file)
        {
            element.SendKeys(file);
        }
    }
}
