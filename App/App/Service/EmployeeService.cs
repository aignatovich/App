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
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net.Configuration;
using System.Web.Configuration;
using System.Net;
using App.Properties;
using Newtonsoft.Json;
using App.Models.AutocompleteQueryModel;

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

        public IPagedList<EmployeeViewModel> GetIPagedList(ManagingRequest request)
        {
            int projectId = (request.ProjectId ?? projectDataAccessObject.GetLastProjectId());
            int page = (request.Page ?? 1);
            int year = (request.Year ?? DateTime.Now.Year);
            int month = (request.Month ?? DateTime.Now.Month);
            ProjectViewModel project = projectId == -1 ? new ProjectViewModel() : ProjectViewModel.Create(projectDataAccessObject.GetSingle(projectId));
            ICollection<EmployeeViewModel> employees = (request.ProjectId == null ? GetAllViewModels() : project.CurrentEmployees);
            ICollection<EmployeeViewModel> toTransfer = employees;

            foreach (EmployeeViewModel e in toTransfer)
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

        public int CalculatePages(int pageSize, int length)
        {
            return (length / pageSize + 1);
        }

        public IEnumerable<SimplifiedEmployeeViewModel> SimplifyCollection(IEnumerable<EmployeeModel> employees)
        {
            ICollection<SimplifiedEmployeeViewModel> employeesSimplified = new List<SimplifiedEmployeeViewModel>();
            IEnumerable<EmployeeModel> employeesNotSimplified = employees;

            foreach (EmployeeModel employee in employeesNotSimplified)
            {
                employeesSimplified.Add(new SimplifiedEmployeeViewModel(employee));
            }

            return employeesSimplified;
        }
    }
}