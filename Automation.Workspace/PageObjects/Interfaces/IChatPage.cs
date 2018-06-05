using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Workspace.PageObjects.Interfaces
{
    public interface IChatPage
    {
        void SendKeysToMessageInput(string keys);
        void ClickSendButton();
        void ClickCallButton();
        void ClickDisconnectCallButton();
    }
}
