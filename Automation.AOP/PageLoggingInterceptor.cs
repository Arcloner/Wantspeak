using Automation.Framework.PageObjects.Base;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.AOP
{
    public class PageLoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            bool ExecutedWithoutExeptions = false;
            while (ExecutedWithoutExeptions == false)
            {
                try
                {
                    ((BasePage)invocation.InvocationTarget).SwitchWindow();
                    ((BasePage)invocation.InvocationTarget).GetWaiter().WaitForDOMLoad();
                    ((BasePage)invocation.InvocationTarget).GetWaiter().WaitForAjaxLoad();
                    invocation.Proceed();
                    ExecutedWithoutExeptions = true;
                }
                catch (Exception e)
                {
                    //Exception                    
                }
                finally
                {
                    //Exiting Method
                }
            }
        }
    }
}
