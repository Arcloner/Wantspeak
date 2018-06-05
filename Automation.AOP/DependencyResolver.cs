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
        private IWindsorContainer _container;

        public DependencyResolver()
        {
            if (_container == null)
            {
                _container = new WindsorContainer();                
            }
        }

        public void Register(IRegistration component)
        {            
            _container.Register(component);
        }

        //Resolve types
        public T For<T>(Guid DriverKey)
        {
            Dictionary<string, object> arguments = new Dictionary<string, object>();
            arguments.Add("DriverKey", DriverKey);
            return _container.Resolve<T>(arguments);
        }
    }
}
