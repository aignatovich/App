using App.Models;
using System.Web.Mvc;
using App.Service;
using PagedList;
using System;
using System.Web;
using App.Service.Interfaces;
using CodeFirst;
using System.Collections.Generic;
using Newtonsoft.Json;
using App.Models.JqGridObjects;
using App.ModelBindings;
using App.ModelBinding;

namespace App.Controllers
{
    public class EmployeeController : Controller
    {

        private IEmployeeService employeeService;
        private IProjectService projectService;
        private IEmployeeTableService tableService;
        private IBroadcastService broadcastService;
        private IAutocompleteEmployeeService employeeAutocomplete;

        public EmployeeController(IEmployeeService employeeService, IProjectService projectService, 
            IEmployeeTableService jqGridService, IBroadcastService broadcastService, IAutocompleteEmployeeService employeeAutocomplete)
        {
            this.employeeService = employeeService;
            this.projectService = projectService;
            this.tableService = jqGridService;
            this.broadcastService = broadcastService;
            this.employeeAutocomplete = employeeAutocomplete;
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateEmployee(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                employeeService.Add(employee);
                return RedirectToAction("ShowEmployees");
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult ShowEmployees(int ? id)
        {
            return View(id);
        }

        [HttpGet]
        [Authorize]
        public ActionResult RemoveEmployee(int id)
        {
            EmployeeViewModel employee = employeeService.GetSingle(id);
            return View(employee);
        }

        [HttpPost]
        [Authorize]
        public ActionResult RemoveEmployee(EmployeeViewModel employee)
        {
            employeeService.Remove(employee);
            return RedirectToAction("ShowEmployees");
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditEmployee(int id)
        {
            EmployeeViewModel employee = employeeService.GetSingle(id);
            return View(employee);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditEmployee(EmployeeViewModel employee)
        {
            employeeService.Edit(employee);
            return RedirectToAction("ShowEmployees");          
        }

        [HttpGet]
        [Authorize]
        public ActionResult Manage(ManagingRequest request)
        {
            return View(employeeService.GetTableData(request));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public void ApplyChanges(ManagingDateModel model)
        {
            employeeService.ApplyAbsence(model);
        }

        [HttpGet]
        [Authorize]
        public string GetEmployeeData([ModelBinder(typeof(TableRequestBinder))] TableRequest request)
        {
            var toTransfer = tableService.Create(request);
            return JsonConvert.SerializeObject(toTransfer);
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public void Broadcast([ModelBinder(typeof(IdsArrayBinder))] IEnumerable<Int32> ids, string message)
        {
            broadcastService.Broadcast(ids, message);
        }


        [Authorize]
        public string NameAutocompleteService(string query)
        {
            return employeeAutocomplete.FormAutocompleteResponseByName(query);
        }

        [Authorize]
        public string SurnameAutocompleteService(string query)
        {
            return employeeAutocomplete.FormAutocompleteResponseBySurname(query);
        }


    }
}