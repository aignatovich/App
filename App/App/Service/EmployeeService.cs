using App.DAL;
using App.Models;
using App.Models.ManagingTableModels;
using App.Service.Interfaces;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Models.JqGridObjects;
using App.Models.EmployeeModels;

namespace App.Service
{
    public class EmployeeService:IEmployeeService
    {
        private IEmployeeDAO employeeDataAccessObject;
        private IProjectDAO projectDataAccessObject;

        private const int pageSize = 25;

        public EmployeeService(IEmployeeDAO employeeDataAccessObject, IProjectDAO projectDataAccessObject)
        {
            this.employeeDataAccessObject = employeeDataAccessObject;
            this.projectDataAccessObject = projectDataAccessObject;
        }

        public ICollection<EmployeeModel> GetEmployeesByIds(IEnumerable<Int32> ids)
        {
            ICollection<EmployeeModel> employees = new List<EmployeeModel>();

            foreach (Int32 employeeId in ids)
            {
                employees.Add(employeeDataAccessObject.GetSingle(employeeId));
            }

            return employees;
        }

        public void Add(EmployeeViewModel employee)
        {
            EmployeeModel toTransfer = employee.AsEmployeeModel();
            employeeDataAccessObject.Add(toTransfer);
        }

        public ICollection<EmployeeViewModel> GetAllViewModels()
        {
            ICollection<EmployeeViewModel> toTransfer = new List<EmployeeViewModel>();
            ICollection<EmployeeModel> employees = employeeDataAccessObject.GetAll();

            foreach (EmployeeModel e in employees)
            {
                toTransfer.Add(new EmployeeViewModel(e));
            }

            return toTransfer;
        }

        public EmployeeViewModel GetSingle(int id)
        {
            return new EmployeeViewModel(employeeDataAccessObject.GetSingle(id));
        }

        public void Remove(EmployeeViewModel employee)
        {
            employeeDataAccessObject.Remove(employee.Id);
        }

        public void Edit(EmployeeViewModel employee)
        {
            EmployeeModel toTransfer = employee.AsEmployeeModel();
            employeeDataAccessObject.Edit(toTransfer);
        }

        public IPagedList<EmployeeViewModel> GetAllAsIPagedList(ManagingRequest request)
        {
            int pageNumber = (request.Page ?? 1);
            int sortingOrder = (request.Sort ?? 2);
            int month = (request.Month ?? DateTime.Now.Month);
            int year = (request.Year ?? DateTime.Now.Year);
            int? projectId = request.ProjectId;

            return GetIPagedList(month, year, pageNumber, sortingOrder, projectId);
        }

        public IPagedList<EmployeeViewModel> GetAllAsIPagedList(int? monthTransfered, int? yearTransfered, int? page, int? sorting)
        {
            int pageNumber = (page ?? 1);
            int sortingOrder = (sorting ?? 2);
            int month = (monthTransfered ?? 0);
            int year = (yearTransfered ?? 0);
            int? projectId = null;

            return GetIPagedList(month, year, pageNumber, sortingOrder, projectId);

        }

        public IPagedList<EmployeeViewModel> GetIPagedList(int month, int year, int page, int sortingOrder, int? id)
        {
            int projectId = (id ?? projectDataAccessObject.GetLastProjectId());
            ProjectViewModel project = projectId == -1 ? new ProjectViewModel() : ProjectViewModel.Create(projectDataAccessObject.GetSingle(projectId));
            ICollection<EmployeeViewModel> employees = (id == null ? GetAllViewModels() : project.CurrentEmployees);
            List<EmployeeViewModel> toTransfer = new List<EmployeeViewModel>();

            switch (sortingOrder)
            {
                case 1:
                    toTransfer = employees.OrderBy(x => x.Position.ToString()).ToList();
                    break;
                case 2:
                    toTransfer = employees.OrderBy(x => x.Name).ToList();
                    break;
                case 3:
                    toTransfer = employees.OrderBy(x => x.Surname).ToList();
                    break;
            }

            foreach (EmployeeViewModel e in toTransfer)
            {
                e.AbsenceList = e.AbsenceList.Where(x => (x.Month == month && x.Year == year)).ToList();
            }       
            return toTransfer.ToPagedList(page, pageSize);
        }

        public TableData GetTableData(ManagingRequest request)
        {
            return new TableData(GetAllAsIPagedList(request),request);
        }


        public void ApplyAbsence(ManagingDateModel model)
        {
            int id = model.UserId;

            EmployeeModel employee = employeeDataAccessObject.GetSingle(id);
            employee.AbsenceList.Add(new ManagingDateModel {
                Day = model.Day,
                Month = model.Month,
                Year = model.Year,
                Reason = (Reason)model.Reason,
                ProjectId = model.ProjectId
            });
        }

        public int CalculateTotalPages(int pageSize)
        {
            return (employeeDataAccessObject.GetAll().Count / pageSize + 1);
        }

        public int CalculatePages(int pageSize, int length)
        {
            return (length / pageSize + 1);
        }

        public ICollection<SimplifiedEmployeeViewModel> GetAllSimplified()
        {
            ICollection<SimplifiedEmployeeViewModel> employeesSimplified = new List<SimplifiedEmployeeViewModel>();
            ICollection<EmployeeModel> employeesNotSimplified = employeeDataAccessObject.GetAll();

            foreach (EmployeeModel employee in employeesNotSimplified)
            {
                employeesSimplified.Add(SimplifiedEmployeeViewModel.Create(employee));
            }

            return employeesSimplified;
        }

    }
}