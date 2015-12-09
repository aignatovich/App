using App.Models;
using App.Models.ManagingTableModels;
using App.Service.Interfaces;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Service
{
    public class ManagingTableService:IManagingTableService
    {
        private IProjectService projectService;

        public ManagingTableService(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public TableData CreateTable(IPagedList<EmployeeViewModel> employees, ManagingRequest request)
        {
            int year = (request.Year ?? DateTime.Now.Year);
            int month = (request.Month ?? DateTime.Now.Month);
            int sort = (request.Sort ?? 2);
            int? projectId = request.ProjectId;
            int pageNumber = employees.PageNumber;
            int pageSize = employees.PageSize;
            string projectName = projectId == null ? "" : projectService.GetSingle((int)projectId).Name;

            if (request.Role != null && !request.Role.Equals(Roles.All))
            {
                employees = employees.Where(x => x.Position.ToString().Equals(request.Role.ToString())).ToPagedList(employees.PageNumber, employees.PageSize);
            }

            return new TableData()
            {
                ProjectId = projectId,
                CurrentProjectName = projectName,
                Role = request.Role,
                Projects = projectService.GetAllViewModels(),
                Employees = employees,
                Month = (Month)month,
                Year = year,
                DayLimit = DateTime.DaysInMonth(year, month),
                FirstDay = (DayEnum)(int)(new DateTime(year, month, 1)).DayOfWeek,
                StartYear = 2010,
                EndYear = 2015,
                Sort = (SortEnum)sort
            };
        }
    }
}
