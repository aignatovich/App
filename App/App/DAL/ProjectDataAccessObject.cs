using System.Collections.Generic;
using System.Linq;
using App.Models;
using CodeFirst;

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
            var editableProject = DatabaseModelContainer.Current.ProjectSet.FirstOrDefault(x => x.Id == project.Id);
            editableProject.Name = project.Name;
            editableProject.StartDate = project.StartDate;
            editableProject.EndDate = project.EndDate;
        }

        public void Remove(int id)
        {
            var project = DatabaseModelContainer.Current.ProjectSet.FirstOrDefault(x => x.Id == id);
            DatabaseModelContainer.Current.ProjectSet.Remove(project);
        }

        public ICollection<ProjectModel> GetAll()
        {
            ICollection<ProjectModel> projectList = DatabaseModelContainer.Current.ProjectSet.ToList();
            return projectList;
        }

        public ProjectModel GetSingle(int id)
        {
            var project = DatabaseModelContainer.Current.ProjectSet.FirstOrDefault(x => x.Id == id);
            return project;
        }

        public bool Exists(ProjectModel project)
        {
            return (DatabaseModelContainer.Current.ProjectSet.Any(x =>
                               x.Name.Equals(project.Name) &&
                               x.StartDate.Equals(project.StartDate) &&
                               x.EndDate == (project.EndDate) && x.Id != project.Id));
        }

        public int GetLastProjectId()
        {
            var project = DatabaseModelContainer.Current.ProjectSet.OrderByDescending(x => x.Id).FirstOrDefault();
            return project?.Id ?? -1;
        }

        public IEnumerable<ProjectModel> Search(string query)
        {
            return DatabaseModelContainer.Current.ProjectSet.Where(x => (x.Name.Contains(query) || 
            x.StartDate.ToString().Contains(query) || x.EndDate.ToString().Contains(query)));
        }

        public int GetTotalEmployeeCount(int? projectId)
        {
            return projectId == null ? DatabaseModelContainer.Current.EmployeeSet.Count() : DatabaseModelContainer.Current.ProjectSet.FirstOrDefault(x => x.Id == projectId).CurrentEmployees.Count;
        }

    }
}