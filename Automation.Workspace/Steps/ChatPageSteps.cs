using Automation.AOP;
using Automation.Workspace.PageObjects.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Workspace.Steps
{
    public class ChatPageSteps
    {
        IChatPage chatPage;

        public ChatPageSteps(Guid driverId,DependencyResolver resolver)
        {
            chatPage = resolver.For<IChatPage>(driverId);
        }

        public void SendMessage(string message)
        {
            chatPage.SendKeysToMessageInput(message);
            chatPage.ClickSendButton();
        }

        public void CallUser()
        {
            chatPage.ClickCallButton();
        }

        public void DisconnectCall()
        {
            chatPage.ClickDisconnectCallButton();
        }
    }
}
