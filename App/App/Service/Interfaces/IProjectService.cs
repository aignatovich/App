using System;
using System.Collections.Generic;
using App.Models;
using PagedList;

namespace App.Service.Interfaces
{
    public interface IProjectService
    {
        ICollection<ProjectViewModel> GetAllViewModels();

        void EmployInProject(int projectId, IEnumerable<Int32> ids);

        void Add(ProjectViewModel projectViewModel);

        ProjectViewModel GetSingle(int id);

        void Edit(ProjectViewModel projectViewModel);

        void Remove(ProjectViewModel project);

        int GetLastProjectId();

        IPagedList<ProjectViewModel> GetAllAsIPagedList(int? page, string query);
    }
}
