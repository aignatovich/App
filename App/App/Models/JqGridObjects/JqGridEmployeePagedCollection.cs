using App.Models.EmployeeModels;
using App.Service;
using App.Service.Interfaces;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;
using static App.Util.AutofacConfig;
using Newtonsoft.Json.Linq;

namespace App.Models.JqGridObjects
{
    public class JqGridEmployeePagedCollection
    {
        public int Page { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }

        public string SortColumn { get; set; }

        public string SortOrder { get; set; }

        private static IProjectService projectService { get; set; }

        public IEnumerable<SimplifiedEmployeeViewModel> Employees { get; set; }
    }
}