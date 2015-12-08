using PagedList;
using System;
using App.Service;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Service.Interfaces;
using Autofac;
using static App.Util.AutofacConfig;

namespace App.Models.ManagingTableModels
{
    public class TableData
    {
        public int? ProjectId { get; set; }
        public IPagedList<EmployeeViewModel> Employees { get; set; }

        public Month Month { get; set; }

        public int Year { get; set; }

        public int DayLimit { get; set; }

        public DayEnum FirstDay { get; set; }

        public int StartYear { get; set; }

        public int EndYear { get; set; }

        public SortEnum? Sort { get; set; }

        public Roles? Role { get; set; }

        public ICollection<ProjectViewModel> Projects { get; set; }

        public string CurrentProjectName { get; set; }

        public TableData()
        {

        }     
    }
}