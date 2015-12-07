﻿using App.DAL;
using App.Models;
using App.Models.AutocompleteQueryModel;
using App.Service.Interfaces;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            ICollection<ProjectModel> projectList = projectDataAccessObject.GetAll();
            ICollection<ProjectViewModel> toTransfer = new List<ProjectViewModel>();
            foreach (ProjectModel item in projectList)
            {
                toTransfer.Add(ProjectViewModel.Create(item));
            }
            return toTransfer;
        }

        public void EmployInProject(int projectId, IEnumerable<Int32> ids)
        {
            ProjectModel project = projectDataAccessObject.GetSingle(projectId);
            ICollection<EmployeeModel> employees = employeeDataAccessObject.GetEmployeesByIds(ids);
            project.CurrentEmployees.Clear();
            project.CurrentEmployees = employees;

            foreach (EmployeeModel employee in project.CurrentEmployees)
            {
                employee.ActualProjects.Add(project);
            }
        }

        public void Add(ProjectViewModel projectViewModel)
        {
            ProjectModel projectModel = projectViewModel.AsProjectModel();
            projectDataAccessObject.Add(projectModel);
        }

        public ProjectViewModel GetSingle(int id)
        {
            return new ProjectViewModel(projectDataAccessObject.GetSingle(id));
        }

        public void Edit(ProjectViewModel projectViewModel)
        {
            ProjectModel toEdit =  projectViewModel.AsProjectModel();
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

        public ICollection<ProjectViewModel> Search(ProjectViewModel project)
        {
            ICollection<ProjectViewModel> projects = GetAllViewModels();
            ICollection<ProjectViewModel> toTransfer = new List<ProjectViewModel>();

            toTransfer = projects.Where(x => (x.Name.Equals(project.Name) || 
            x.StartDate.Equals(project.StartDate) || x.EndDate.Equals(project.EndDate))).ToList();            
            return toTransfer;
        }

        public IPagedList<ProjectViewModel> GetAllAsIPagedList(int? page, string query)
        {
            int currentPage = (page ?? 1);

            if (query == null)
            {
                return GetAllViewModels().ToPagedList(currentPage, pageSize);
            }
            else
            {
                return GetAllViewModels().Where(x => (x.Name.Contains(query) || x.StartDate.Contains(query) ||  x.EndDate.Contains(query))).ToPagedList(currentPage, pageSize);
            }    
        }

        public string FormAutocompleteResponse(string query)
        {
            IEnumerable<ProjectModel> projects = projectDataAccessObject.GetAll().Where(x => x.Name.Contains(query));
            List<string> suggestions = new List<string>();

            foreach (ProjectModel project in projects)
            {
                suggestions.Add(project.Name);
            }

            AutocompleteQuery queryModel = new AutocompleteQuery() { query = query, suggestions = suggestions};
            return JsonConvert.SerializeObject(queryModel);
        }

    }
}