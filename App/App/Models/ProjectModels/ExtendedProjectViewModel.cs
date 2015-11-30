using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Models;

namespace App.Models
{
    public class ExtendedProjectViewModel
    {
        public ProjectViewModel Project { get; set; }
        public ICollection<ExtendedEmployeeViewModel> Unemployed { get; set; }
        public ICollection<ExtendedEmployeeViewModel> Employed { get; set; }

        public ExtendedProjectViewModel()
        {
            Employed = new List<ExtendedEmployeeViewModel>();
            Unemployed = new List<ExtendedEmployeeViewModel>();
            Project = new ProjectViewModel();
        }

        public ExtendedProjectViewModel(ICollection<EmployeeViewModel> employees, ProjectViewModel project)
        {
            Employed = new List<ExtendedEmployeeViewModel>();
            Unemployed = new List<ExtendedEmployeeViewModel>();
            Project = project;
            IEnumerable<EmployeeViewModel> employeeModels = employees.Where(x => (!project.CurrentEmployees.Contains(x)));

            foreach (EmployeeViewModel e in employeeModels)
            {
                Unemployed.Add(new ExtendedEmployeeViewModel(e));
            }
            foreach (EmployeeViewModel e in project.CurrentEmployees)
            {
                Employed.Add(new ExtendedEmployeeViewModel(e));
            }
        }

    }
}