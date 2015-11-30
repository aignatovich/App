using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

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