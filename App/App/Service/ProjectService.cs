using System;
using System.Collections.Generic;
using System.Linq;
using App.DAL;
using App.Models;
using App.Service.Interfaces;
using PagedList;

namespace App.Service
{
    public class ProjectService:IProjectService
    {
        private IProjectDAO projectDataAccessObject;
        private IEmployeeDAO employeeDataAccessObject;

        private const int pageSize = 12;

        public ProjectService(IProjectDAO projectDataAccessObject, IEmployeeDAO employeeDataAccessObject)
        {
            this.projectDataAccessObject = projectDataAccessObject;
            this.employeeDataAccessObject = employeeDataAccessObject;
        }

        public ICollection<ProjectViewModel> GetAllViewModels()
        {
            var projectList = projectDataAccessObject.GetAll();
            return projectList.Select(ProjectViewModel.Create).ToList();
        }

        public void EmployInProject(int projectId, IEnumerable<int> ids)
        {
            var project = projectDataAccessObject.GetSingle(projectId);
            var employees = employeeDataAccessObject.GetEmployeesByIds(ids);
            project.CurrentEmployees.Clear();
            project.CurrentEmployees = employees;

            foreach (var employee in project.CurrentEmployees)
            {
                employee.ActualProjects.Add(project);
            }
        }

        public void Add(ProjectViewModel projectViewModel)
        {
            var projectModel = projectViewModel.AsProjectModel();
            projectDataAccessObject.Add(projectModel);
        }

        public ProjectViewModel GetSingle(int id)
        {
            return new ProjectViewModel(projectDataAccessObject.GetSingle(id));
        }

        public void Edit(ProjectViewModel projectViewModel)
        {
            var toEdit =  projectViewModel.AsProjectModel();
            projectDataAccessObject.Edit(toEdit);
        }

        public void Remove(ProjectViewModel project)
        {
            projectDataAccessObject.Remove(project.Id);
        }

        public int GetLastProjectId()
        {
            return projectDataAccessObject.GetLastProjectId();
        }

        public IPagedList<ProjectViewModel> GetAllAsIPagedList(int? page, string query)
        {
            var currentPage = (page ?? 1);

            if (query == null)
            {
                return GetAllViewModels().ToPagedList(currentPage, pageSize);
            }
            else
            {
                var projects = projectDataAccessObject.Search(query);
                var projectViewModels = projects.Select(project => new ProjectViewModel(project)).ToList();
                return projectViewModels.ToPagedList(currentPage, pageSize);
            }    
        }
    }
}