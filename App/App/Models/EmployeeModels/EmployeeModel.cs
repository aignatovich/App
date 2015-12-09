using System.Collections.Generic;

namespace App.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public Roles Position { get; set; }

        public virtual ICollection<ProjectModel> ActualProjects { get; set; }

        public virtual ICollection<ManagingDateModel> AbsenceList { get; set; }

        public EmployeeModel()
        {
            ActualProjects = new List<ProjectModel>();
            AbsenceList = new List<ManagingDateModel>();
        }
    }
    
}