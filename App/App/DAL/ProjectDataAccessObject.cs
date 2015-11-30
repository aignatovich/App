using App.Models;
using CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.DAL
{
    public class ProjectDataAccessObject:IProjectDAO
    {
        public void Add(ProjectModel project)
        {
            DatabaseModelContainer.Current.ProjectSet.Add(project);
        }

        public void Edit(ProjectModel project)
        {
            ProjectModel editableProject = DatabaseModelContainer.Current.ProjectSet.Where(x => x.Id == project.Id).FirstOrDefault();
            editableProject.Name = project.Name;
            editableProject.StartDate = project.StartDate;
            editableProject.EndDate = project.EndDate;
        }

        public void Remove(int id)
        {
            ProjectModel project = DatabaseModelContainer.Current.ProjectSet.Where(x => x.Id == id).FirstOrDefault();
            DatabaseModelContainer.Current.ProjectSet.Remove(project);
        }

        public ICollection<ProjectModel> GetAll()
        {
            ICollection<ProjectModel> projectList = DatabaseModelContainer.Current.ProjectSet.ToList();
            return projectList;
        }

        public ProjectModel GetSingle(int id)
        {
            ProjectModel project = DatabaseModelContainer.Current.ProjectSet.Where(x => x.Id == id).FirstOrDefault();
            return project;
        }

        public bool Exists(ProjectModel project)
        {
            return (DatabaseModelContainer.Current.ProjectSet.Any(x =>
                               x.Name.Equals(project.Name) &&
                               x.StartDate.Equals(project.StartDate) &&
                               x.EndDate == (project.EndDate) && !(x.Id == project.Id)));
        }

        public int GetLastProjectId()
        {
            ProjectModel project = DatabaseModelContainer.Current.ProjectSet.OrderByDescending(x => x.Id).FirstOrDefault();
            return (project == null ? -1 : project.Id);
        }
    }
}