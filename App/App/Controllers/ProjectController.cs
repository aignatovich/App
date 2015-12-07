using App.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using App.Service;
using App.ModelBinding;
using App.Service.Interfaces;
using Newtonsoft.Json;
using PagedList;

namespace App.Controllers
{
    public class ProjectController : Controller
    {
        private IProjectService projectService;
        private IEmployeeService employeeService;
        private IExtendedProjectService extendedProjectService;

        public ProjectController(IProjectService projectService, IEmployeeService employeeService, IExtendedProjectService extendedProjectService)
        {
            this.extendedProjectService = extendedProjectService;
            this.employeeService = employeeService;
            this.projectService = projectService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateProject(ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                projectService.Add(project);
                return RedirectToAction("ShowProjects");
            }

            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult ShowProjects(int? page, string query)
        {
            IPagedList<ProjectViewModel> toTransfer = projectService.GetAllAsIPagedList(page, query);
            return View(toTransfer);
        }

        [HttpGet]
        [Authorize]
        public ActionResult RemoveProject(int id)
        {
            ProjectViewModel project = projectService.GetSingle(id);
            return View(project);
        }

        [HttpPost]
        [Authorize]
        public ActionResult RemoveProject(ProjectViewModel project)
        {
            projectService.Remove(project);
            return Redirect("ShowProjects");
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditProject(int id)
        {
            ProjectViewModel toTransfer = projectService.GetSingle(id);
            return View(toTransfer);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditProject(ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                projectService.Edit(project);
                return RedirectToAction("ShowProjects");
            }

            return View("EditProject", project);
        }

        [HttpGet]
        [Authorize]
        public ActionResult SetupProject(int id)
        {
            ExtendedProjectViewModel toTransfer = extendedProjectService.Create(id);
            return View(toTransfer);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SetupProject([ModelBinder(typeof(IdsArrayBinder))] IEnumerable<Int32> ids, int projectId)
        {
            projectService.EmployInProject(projectId, ids);
            return RedirectToAction("ShowProjects");
        }

        [Authorize]
        public string AutocompleteService(string query)
        {
            return projectService.FormAutocompleteResponse(query);
        }
    }                
}