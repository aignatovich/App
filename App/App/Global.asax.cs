﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using App.ModelBinding;
using App.ModelBindings;
using App.Models.JqGridObjects;
using App.Util;
using CodeFirst;

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
            ModelBinders.Binders.Add(typeof(TableRequest), new TableRequestBinder());
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
