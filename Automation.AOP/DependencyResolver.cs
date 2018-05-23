using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.AOP
{
    public class DependencyResolver
    {
        private static IWindsorContainer _container;

        static DependencyResolver()
        {
            if (_container == null)
            {
                _container = new WindsorContainer();                
            }
        }

        public static void Register(IRegistration component)
        {
            _container.Register(component);
        }

        //Resolve types
        public static T For<T>(Guid DriverKey)
        {
            Dictionary<string, object> arguments = new Dictionary<string, object>();
            arguments.Add("DriverKey", DriverKey);
            return _container.Resolve<T>(arguments);
        }
    }
}
