using System.Data.Entity;
using CodeFirst;

namespace App.Models.DatabaseModel
{
    public class DatabaseContextAccessor:IDatabaseContextAccessor
    {
        private DatabaseModelContainer dbContext;

        public DatabaseContextAccessor()
        {
            dbContext = DatabaseModelContainer.Current;
        }

        public DbSet<EmployeeModel> GetEmployeeSet()
        {
            return this.dbContext.EmployeeSet;
        }

        public DbSet<ProjectModel> GetProjectSet()
        {
            return this.dbContext.ProjectSet;
        }
    }
}