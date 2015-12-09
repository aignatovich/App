using System.Data.Entity;
using System.Web;
using App.Models;

namespace CodeFirst
{
    public class DatabaseModelContainer : DbContext
    {
        public static DatabaseModelContainer Current => (HttpContext.Current.Items["_DatabaseModelContainer"] as DatabaseModelContainer);

        public DatabaseModelContainer()
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseModelContainer>());
            Database.SetInitializer<DatabaseModelContainer>(null);            
        }

        public DbSet<EmployeeModel> EmployeeSet { get; set; }
        public DbSet<ProjectModel> ProjectSet { get; set; }
    }
}