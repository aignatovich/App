using App.Models.EmployeeModels;
using App.Models.JqGridObjects;
using App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;

namespace App.Service
{
    public class JqGridService:IJqGridService
    {
        private const int jqGridPageSize = 100;
        private const string SortingOrderDesc = "desc";
        private const string SortingOrderAsc = "asc";

        private IEmployeeService employeeService;
        private IProjectService projectService;

        public JqGridService(IEmployeeService employeeService, IProjectService projectService)
        {
            this.employeeService = employeeService;
            this.projectService = projectService;
        }

        public JqGridEmployeePagedCollection Create(JqGridRequest request)
        {

            List<SimplifiedEmployeeViewModel> employees;

            if (request.ProjectId == -1)
            {
                employees = employeeService.GetAllSimplified().ToList();
            }
            else
            {
                employees = employeeService.SimplifyCollection(projectService.GetSingle(request.ProjectId).CurrentEmployees).ToList();
            }

            bool search = request.IsSearch;
            int page = request.Page;
            string sortingOrder = request.SortOrder;
            string sortingProperty = request.SortingProperty;

            if (!search)
            {
                int startIndex = (page - 1) * jqGridPageSize;
                int endIndex = jqGridPageSize < (employees.Count - (page - 1) * jqGridPageSize) ? jqGridPageSize : (employees.Count - (page - 1) * jqGridPageSize);

                IEnumerable<SimplifiedEmployeeViewModel> toTransfer = employees.GetRange(startIndex, endIndex);
                toTransfer = OrderByProperty(toTransfer, sortingOrder, sortingProperty);

                return new JqGridEmployeePagedCollection()
                {
                    Employees = toTransfer,
                    Page = page,
                    TotalPages = employeeService.CalculatePages(jqGridPageSize, employees.Count),
                    SortColumn = sortingProperty,
                    SortOrder = sortingOrder,
                    TotalRecords = employees.Count
                };
            }

            else
            {
                IEnumerable<SimplifiedEmployeeViewModel> toTransfer = employees;

                toTransfer = DirectSearch(toTransfer, request.Name, request.Surname, request.Id, request.Role);
                int startIndex = (page - 1) * jqGridPageSize;
                int endIndex = jqGridPageSize < (toTransfer.Count() - (page - 1) * jqGridPageSize) ? jqGridPageSize : (toTransfer.Count() - (page - 1) * jqGridPageSize);
                toTransfer = OrderByProperty(toTransfer, sortingOrder, sortingProperty);

                return new JqGridEmployeePagedCollection()
                {
                    Employees = toTransfer.ToList().GetRange(startIndex, endIndex),
                    Page = page,
                    TotalPages = employeeService.CalculatePages(jqGridPageSize, toTransfer.Count()),
                    SortColumn = sortingProperty,
                    SortOrder = sortingOrder,
                    TotalRecords = toTransfer.Count()
                };
            }
        }

        private ICollection<SimplifiedEmployeeViewModel> DirectSearch(IEnumerable<SimplifiedEmployeeViewModel> employees, string name, string surname, int? id, string role)
        {
            IEnumerable<SimplifiedEmployeeViewModel> toTransfer = employees;

            if (id != null)
            {
                toTransfer = toTransfer.Where(x => x.Id.Equals(id)).ToList();
            }
            if (role != "")
            {
                toTransfer = toTransfer.Where(x => x.PositionValue.Equals(role)).ToList();
            }

            if (name != "")
            {
                toTransfer = toTransfer.Where(x => x.Name.Contains(name)).ToList();
            }
            if (surname != "")
            {
                toTransfer = toTransfer.Where(x => x.Surname.Contains(surname)).ToList();
            }

            return toTransfer.ToList();
        }

        private ICollection<SimplifiedEmployeeViewModel> OrderByProperty(IEnumerable<SimplifiedEmployeeViewModel> toTransfer, string sortingOrder, string property)
        {
            if (sortingOrder.Equals(SortingOrderAsc))
            {
                toTransfer = toTransfer.AsEnumerable().OrderBy(property).Reverse();
            }
            else
            {
                toTransfer = toTransfer.AsEnumerable().OrderBy(property);
            }

            return toTransfer.ToList();
        }
    }

}
