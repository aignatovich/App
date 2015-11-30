using CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

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