using Automation.Framework.Drivers;
using Automation.Framework.PageObjects.Base;
using Automation.Workspace.PageObjects.Interfaces;
using OpenQA.Selenium;
using System;

namespace Automation.Workspace.PageObjects
{
    public  class RegistrationPage:BasePage, IRegisterPage
    {
        public RegistrationPage(Guid DriverKey) : base(DriverKey) { }

        private IWebElement nickName => Driver.GetDriver(DriverKey).FindElementById("Nickname");

        private IWebElement yearsOld => Driver.GetDriver(DriverKey).FindElementById("Old");

        private IWebElement city=> Driver.GetDriver(DriverKey).FindElementById("City");

        private IWebElement submitButton=> Driver.GetDriver(DriverKey).FindElementByClassName("SubmitBTN");

        public void SendKeysToNickName(string keys)
        {
            Perform.SendKeys(nickName, keys);
        }

        public void SendKeysToYearsOld(string keys)
        {
            Perform.SendKeys(yearsOld, keys);
        }

        public void SendKeysToCity(string keys)
        {
            Perform.SendKeys(city, keys);
        }

        public void ClickSubmit()
        {
            Perform.Click(submitButton);
        }
    }
}
