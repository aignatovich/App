using App.Models;
using System.Data.Entity;
using System.Web;

namespace CodeFirst
{
    public class DatabaseModelContainer : DbContext
    {

        public static DatabaseModelContainer Current
        {
            get { return HttpContext.Current.Items["_DatabaseModelContainer"] as DatabaseModelContainer; }
        }
        public DatabaseModelContainer()           
        {
            Database.SetInitializer<DatabaseModelContainer>(null);
            //Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseModelContainer>());
        }

        public DbSet<EmployeeModel> EmployeeSet { get; set; }
        public DbSet<ProjectModel> ProjectSet { get; set; }
    }

   
}