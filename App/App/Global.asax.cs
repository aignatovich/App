using App.ModelBinding;
using App.Models;
using App.Util;
using CodeFirst;
using FluentValidation.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace App
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.ConfigureContainer();           
            ModelBinders.Binders.Add(typeof(IEnumerable<Int32>), new IdsArrayBinder());     
        }

        protected virtual void Application_BeginRequest()
        {
            HttpContext.Current.Items["_DatabaseModelContainer"] = new DatabaseModelContainer();
        }

        protected virtual void Application_EndRequest()
        {
            var entityContext = HttpContext.Current.Items["_DatabaseModelContainer"] as DatabaseModelContainer;
            if (entityContext != null)
            {
                entityContext.SaveChanges();
                entityContext.Dispose();
            }
        }
    }
}
