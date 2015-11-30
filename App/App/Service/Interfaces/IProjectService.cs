using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        ICollection<ProjectViewModel> Search(ProjectViewModel project);
    }
}
