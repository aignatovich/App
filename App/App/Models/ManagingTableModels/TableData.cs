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

        public int Sort { get; set; }

        public ICollection<ProjectViewModel> Projects { get; set; }

        public TableData()
        {
        }

        public TableData(IPagedList<EmployeeViewModel> employees, ManagingRequest request)
        {

            IProjectService projectService = Container.Resolve<IProjectService>();
            int year = (request.Year ?? DateTime.Now.Year);
            int month = (request.Month ?? DateTime.Now.Month);
            int sort = (request.Sort ?? 2);
            int projectId = (request.ProjectId ?? projectService.GetLastProjectId());

            Projects = projectService.GetAllViewModels();
            Employees = employees;
            Month = (Month)month;
            Year = year;
            DayLimit = DateTime.DaysInMonth(year, month);
            FirstDay = (DayEnum)(int)(new DateTime(year, month, 1)).DayOfWeek;
            StartYear = 2010;
            EndYear = 2015;
            Sort = sort;

            ProjectId = projectId;

        }
    }
}