using System.Data.Entity;

namespace App.Models.DatabaseModel
{
    public interface IDatabaseContextAccessor
    {
        DbSet<EmployeeModel> GetEmployeeSet();
        DbSet<ProjectModel> GetProjectSet();
    }
}
