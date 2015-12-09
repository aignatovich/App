using System.Collections.Generic;
using App.Models;

namespace App.DAL
{
    public interface IProjectDAO
    {
        void Add(ProjectModel project);

        void Edit(ProjectModel project);

        void Remove(int id);

        ICollection<ProjectModel> GetAll();

        ProjectModel GetSingle(int id);

        bool Exists(ProjectModel project);

        int GetLastProjectId();

        IEnumerable<ProjectModel> Search(string query);
    }
}
