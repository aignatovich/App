using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
