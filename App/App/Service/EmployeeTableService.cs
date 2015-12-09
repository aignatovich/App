using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using App.DAL;
using App.Models;
using App.Models.EmployeeModels;
using App.Models.JqGridObjects;
using App.Models.ManagingTableModels;
using App.Service.Interfaces;

namespace App.Service
{
    public class EmployeeTableService:IEmployeeTableService
    {
        private const int gridPageSize = 100;

        private IEmployeeService employeeService;
        private IProjectService projectService;
        private IEmployeeDAO employeeDataAccessObject;
        private IProjectDAO projectDataAccessObject;

        public EmployeeTableService(IEmployeeService employeeService, IProjectService projectService, IEmployeeDAO employeeDAO, IProjectDAO projectDAO)
        {
            this.employeeService = employeeService;
            this.projectService = projectService;
            this.employeeDataAccessObject = employeeDAO;
            this.projectDataAccessObject = projectDAO;
        }

        public EmployeePagedCollection Create(TableRequest request)
        {
            IEnumerable<SimplifiedEmployeeViewModel> toTransfer;
            IEnumerable<EmployeeModel> employees;

            var startIndex = (request.Page - 1) * gridPageSize;
            var totalCount = 0;

            if (!request.IsSearch)
            {
                employees = employeeDataAccessObject.GetNextPage(request.Page, gridPageSize, request.ProjectId, request.SortOrder, request.SortingProperty);
                totalCount = projectDataAccessObject.GetTotalEmployeeCount(request.ProjectId);
            }
            else
            {
                employees = employeeDataAccessObject.DirectSearch(request.Name, request.Surname, request.Id, request.Role);
                totalCount = employees.Count();
                employees.Skip(startIndex).Take(gridPageSize);
            }

            toTransfer = employeeService.SimplifyCollection(employees);

            return new EmployeePagedCollection()
            {
                Employees = toTransfer,
                Page = request.Page,
                TotalPages = employeeService.CalculatePages(gridPageSize, totalCount),
                SortColumn = request.SortingProperty,
                SortOrder = request.SortOrder,
                TotalRecords = totalCount
            };
        }      
    }
}
