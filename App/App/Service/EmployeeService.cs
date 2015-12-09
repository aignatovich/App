using System;
using System.Collections.Generic;
using System.Linq;
using App.DAL;
using App.Models;
using App.Models.EmployeeModels;
using App.Models.ManagingTableModels;
using App.Service.Interfaces;
using PagedList;

namespace App.Service
{
    public class EmployeeService:IEmployeeService
    {
        private IEmployeeDAO employeeDataAccessObject;
        private IProjectDAO projectDataAccessObject;
        private IManagingTableService managingTableService;

        private const int pageSize = 25;

        public EmployeeService(IEmployeeDAO employeeDataAccessObject, IProjectDAO projectDataAccessObject, IManagingTableService managingTableService)
        {
            this.employeeDataAccessObject = employeeDataAccessObject;
            this.projectDataAccessObject = projectDataAccessObject;
            this.managingTableService = managingTableService;
        }

      
        public void Add(EmployeeViewModel employee)
        {
            var toTransfer = employee.AsEmployeeModel();
            employeeDataAccessObject.Add(toTransfer);
        }

        public ICollection<EmployeeViewModel> GetAllViewModels()
        {
            var employees = employeeDataAccessObject.GetAll();
            return employees.Select(e => new EmployeeViewModel(e)).ToList();
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
            var toTransfer = employee.AsEmployeeModel();
            employeeDataAccessObject.Edit(toTransfer);
        }

        public IPagedList<EmployeeViewModel> GetIPagedList(ManagingRequest request)
        {
            var projectId = (request.ProjectId ?? projectDataAccessObject.GetLastProjectId());
            var page = (request.Page ?? 1);
            var year = (request.Year ?? DateTime.Now.Year);
            var month = (request.Month ?? DateTime.Now.Month);
            var project = projectId == -1 ? new ProjectViewModel() : ProjectViewModel.Create(projectDataAccessObject.GetSingle(projectId));
            var employees = (request.ProjectId == null ? GetAllViewModels() : project.CurrentEmployees);
            var toTransfer = employees;

            foreach (var e in toTransfer)
            {
                e.AbsenceList = e.AbsenceList.Where(x => (x.Month == month && x.Year == year)).ToList();
            }       
            return toTransfer.ToPagedList(page, pageSize);
        }

        public TableData GetTableData(ManagingRequest request)
        {
            return managingTableService.CreateTable(GetIPagedList(request),request);
        }


        public void ApplyAbsence(ManagingDateModel model)
        {
            var id = model.UserId;

            var employee = employeeDataAccessObject.GetSingle(id);
            employee.AbsenceList.Add(new ManagingDateModel {
                Day = model.Day,
                Month = model.Month,
                Year = model.Year,
                Reason = (Reason)model.Reason,
                ProjectId = model.ProjectId
            });
        }

        public int CalculatePages(int pageSize, int length)
        {
            return (length / pageSize + 1);
        }

        public IEnumerable<SimplifiedEmployeeViewModel> SimplifyCollection(IEnumerable<EmployeeModel> employees)
        {
            var employeesNotSimplified = employees;
            return employeesNotSimplified.Select(employee => new SimplifiedEmployeeViewModel(employee)).ToList();
        }
    }
}