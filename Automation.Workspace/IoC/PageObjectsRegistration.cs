using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Automation.AOP;
using Automation.Workspace.PageObjects;
using Automation.Workspace.PageObjects.Interfaces;

namespace Automation.Workspace.IoC
{
    public class PageObjectsRegistration: IRegistration
    {
        public void Register(IKernelInternal kernel)
        {
            kernel.Register(
                Component.For<PageLoggingInterceptor>()
                    .ImplementedBy<PageLoggingInterceptor>());


            //Pages registration
            kernel.Register(
            Component.For<IStartPage>()
                     .ImplementedBy<StartPage>()
                     .Interceptors(InterceptorReference.ForType<PageLoggingInterceptor>()).Anywhere);

            kernel.Register(
           Component.For<IResultsOfSearch>()
                    .ImplementedBy<ResultsOfSearch>()
                    .Interceptors(InterceptorReference.ForType<PageLoggingInterceptor>()).Anywhere);

        }
    }
}
