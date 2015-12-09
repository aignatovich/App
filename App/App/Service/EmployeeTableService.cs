using App.Models.EmployeeModels;
using App.Models.JqGridObjects;
using App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;
using App.Models;
using App.Models.ManagingTableModels;
using App.DAL;

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

            int startIndex = (request.Page - 1) * gridPageSize;
            int totalCount = 0;

            if (!request.IsSearch)
            {
                if (request.ProjectId == null)
                {
                    employees = employeeDataAccessObject.GetNextPage(request.Page, gridPageSize);
                    toTransfer = employeeService.SimplifyCollection(employees);
                    totalCount = employeeDataAccessObject.GetTotalEmployeeCount();
                }
                else
                {
                    employees = projectDataAccessObject.GetSingle((int)request.ProjectId).CurrentEmployees;
                    totalCount = employees.Count();
                    toTransfer = employeeService.SimplifyCollection(employees);
                    toTransfer = toTransfer.Skip(startIndex).Take(gridPageSize);
                }
            }
            else
            {
                employees = employeeDataAccessObject.DirectSearch(request.Name, request.Surname, request.Id, request.Role);
                totalCount = employees.Count();
                toTransfer = employeeService.SimplifyCollection(employees);
                toTransfer = toTransfer.Skip(startIndex).Take(gridPageSize);
            }

            toTransfer = OrderByProperty(toTransfer, request.SortOrder, request.SortingProperty);
          
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

        private IEnumerable<SimplifiedEmployeeViewModel> OrderByProperty(IEnumerable<SimplifiedEmployeeViewModel> toTransfer, SortEnum sortingOrder, string property)
        {
            if (sortingOrder.Equals(SortEnum.asc))
            {
                toTransfer = toTransfer.OrderBy(property).Reverse();
            }
            else
            {
                toTransfer = toTransfer.OrderBy(property);
            }

            return toTransfer;
        }
    }
}
