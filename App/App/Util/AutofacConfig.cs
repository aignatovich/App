using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Service;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using App.Service.Interfaces;
using App.Models;
using App.DAL;
using CodeFirst;
using App.Models.DatabaseModel;

namespace App.Util
{

    public class AutofacConfig
    {
        public static ContainerBuilder Builder;
        public static IContainer Container;
        public static void ConfigureContainer()
        {
            Builder = new ContainerBuilder();

            Builder.RegisterControllers(typeof(MvcApplication).Assembly);
            Builder.RegisterType<EmployeeService>().As<IEmployeeService>();
            Builder.RegisterType<ExtendedProjectService>().As<IExtendedProjectService>();

            Builder.RegisterType<ProjectService>().As<IProjectService>();
            Builder.RegisterType<PositionsService>().As<IPositionsService>();

            Builder.RegisterType<EmployeeDataAccessObject>().As<IEmployeeDAO>();
            Builder.RegisterType<ProjectDataAccessObject>().As<IProjectDAO>();

            //Builder.RegisterType<DatabaseModelContainer>().As<IDatabase>().InstancePerRequest();
            Builder.RegisterType<DatabaseContextAccessor>().As<IDatabaseContextAccessor>();

            Container = Builder.Build();
           
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}