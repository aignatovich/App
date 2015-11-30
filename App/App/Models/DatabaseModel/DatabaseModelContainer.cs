using App.Models;
using App.Models.DatabaseModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;

namespace CodeFirst
{
    public class DatabaseModelContainer : DbContext
    {
        public static DatabaseModelContainer Current
        {
            get
            {
               return (HttpContext.Current.Items["_DatabaseModelContainer"] as DatabaseModelContainer);
            }
        }
        public DatabaseModelContainer()
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseModelContainer>());
            Database.SetInitializer<DatabaseModelContainer>(null);            
        }

        public DbSet<EmployeeModel> EmployeeSet { get; set; }
        public DbSet<ProjectModel> ProjectSet { get; set; }
    }
}