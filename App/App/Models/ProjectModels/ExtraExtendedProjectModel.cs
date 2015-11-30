using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class ExtraExtendedProjectViewModel
    {
        public ICollection<EmployeeModel> Employees { get; set; }
        public ICollection<ProjectViewModel> Projects { get; set; }

        public ExtraExtendedProjectViewModel()
        {
            Employees = new List<EmployeeModel>();
            Projects = new List<ProjectViewModel>();
        }

        public ExtraExtendedProjectViewModel(ICollection<EmployeeModel> employees, ICollection<ProjectViewModel> project)
        {
            Employees = employees;
            Projects = Projects;
        }
    }
}