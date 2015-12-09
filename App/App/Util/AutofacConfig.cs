using System.Web.Mvc;
using App.DAL;
using App.Models;
using App.Models.DatabaseModel;
using App.Service;
using App.Service.Interfaces;
using Autofac;
using Autofac.Integration.Mvc;

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

            Builder.RegisterType<DatabaseContextAccessor>().As<IDatabaseContextAccessor>();
            Builder.RegisterType<EmployeeTableService>().As<IEmployeeTableService>();

            Builder.RegisterType<ManagingTableService>().As<IManagingTableService>();
            Builder.RegisterType<BroadcastService>().As<IBroadcastService>();

            Builder.RegisterType<AutocompleteEmployeeService>().As<IAutocompleteEmployeeService>();
            Builder.RegisterType<AutocompleteProjectService>().As<IAutocompleteProjectService>();

            Container = Builder.Build();
           
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}