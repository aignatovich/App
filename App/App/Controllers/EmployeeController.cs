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

namespace App.Controllers
{
    public class EmployeeController : Controller
    {

        private IEmployeeService service;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.service = employeeService;
        }

        [HttpGet]
        public ActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                service.Add(employee);
                return RedirectToAction("ShowEmployees");
            }
            return View();
        }

        [HttpGet]
        public ActionResult ShowEmployees(int? page, int? sort, int? month, int? year)
        {
            IPagedList<EmployeeViewModel> toTransfer = service.GetAllAsIPagedList(month,year, page,sort);
            return View(toTransfer);
        }

        [HttpGet]
        public ActionResult RemoveEmployee(int id)
        {
            EmployeeViewModel employee = service.GetSingle(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult RemoveEmployee(EmployeeViewModel employee)
        {
            service.Remove(employee);
            return RedirectToAction("ShowEmployees");
        }

        [HttpGet]
        public ActionResult EditEmployee(int id)
        {
            EmployeeViewModel employee = service.GetSingle(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult EditEmployee(EmployeeViewModel employee)
        {
            service.Edit(employee);
            return RedirectToAction("ShowEmployees");          
        }

        [HttpGet]
        public ActionResult Manage(ManagingRequest request)
        {
            return View(service.GetTableData(request));
        }

        [HttpPost]
        public ActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        public void ApplyChanges(ManagingDateModel model)
        {
            service.ApplyAbsence(model);
        }

        public string GetEmployeeData()
        {
            ICollection<EmployeeViewModel> employeesToTransfer = service.GetAllViewModels();            
            return JsonConvert.SerializeObject(employeesToTransfer);
        }
    }
}