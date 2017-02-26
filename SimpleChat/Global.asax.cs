using SimpleChat.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Threading;
using Microsoft.AspNet.SignalR;
using SimpleChat.Models;

namespace SimpleChat
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var hub= GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            var binder = new Binder(hub);
            Thread BindingThread = new Thread(binder.BindUsers);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BindingThread.Start();
        }
    }
}
