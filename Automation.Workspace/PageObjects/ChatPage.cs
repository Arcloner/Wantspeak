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
    public class ChatPage : BasePage, IChatPage
    {
        public ChatPage(Guid DriverKey) : base(DriverKey){}

        private IWebElement messageInput => Driver.GetDriver(DriverKey).FindElementByClassName("message-input");

        private IWebElement sendButton => Driver.GetDriver(DriverKey).FindElementByClassName("message-submit");

        private IWebElement videoCallButton => Driver.GetDriver(DriverKey).FindElementById("CallButton");

        private IWebElement disconectCallButton=> Driver.GetDriver(DriverKey).FindElementById("Disconnectvideo");

        public void SendKeysToMessageInput(string keys)
        {
            Perform.SendKeys(messageInput, keys);
        }

        public void ClickSendButton()
        {
            Perform.Click(sendButton);
        }

        public void ClickCallButton()
        {
            Perform.Click(videoCallButton);
        }

        public void ClickDisconnectCallButton()
        {
            Perform.Click(disconectCallButton);
        }
    }
}
