using Automation.AOP;
using Automation.Workspace.PageObjects.Interfaces;
using System;

namespace Automation.Workspace.Steps
{
    public class RegisterPageSteps
    {
        IRegisterPage registerPage;

        public RegisterPageSteps(Guid driverId,DependencyResolver resolver)
        {
            registerPage= resolver.For<IRegisterPage>(driverId);
        }


        public void RegisterUser(string nickName, string yearsOld, string city)
        {
            registerPage.SendKeysToNickName(nickName);
            registerPage.SendKeysToYearsOld(yearsOld);
            registerPage.SendKeysToCity(city);
            registerPage.ClickSubmit();
        }
    }
}
