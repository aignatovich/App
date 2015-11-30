using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Models.DatabaseModel
{
    public interface IDatabaseContextAccessor
    {
        DbSet<EmployeeModel> GetEmployeeSet();
        DbSet<ProjectModel> GetProjectSet();
    }
}
