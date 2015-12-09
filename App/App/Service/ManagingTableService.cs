using System;
using System.Linq;
using App.Models;
using App.Models.ManagingTableModels;
using App.Service.Interfaces;
using PagedList;

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
            var year = (request.Year ?? DateTime.Now.Year);
            var month = (request.Month ?? DateTime.Now.Month);
            var sort = (request.Sort ?? 2);
            var projectId = request.ProjectId;
            var pageNumber = employees.PageNumber;
            var pageSize = employees.PageSize;
            var projectName = (projectId == null ? "" : projectService.GetSingle((int)projectId).Name);

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
