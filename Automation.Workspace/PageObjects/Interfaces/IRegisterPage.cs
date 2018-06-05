using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Workspace.PageObjects.Interfaces
{
    public interface IRegisterPage
    {
        void SendKeysToNickName(string keys);
        void SendKeysToYearsOld(string keys);
        void SendKeysToCity(string keys);
        void ClickSubmit();
    }
}
