using System.Collections.Generic;
using System.Linq;

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

        public ExtendedProjectViewModel(IEnumerable<EmployeeViewModel> employees, ProjectViewModel project)
        {
            Employed = new List<ExtendedEmployeeViewModel>();
            Unemployed = new List<ExtendedEmployeeViewModel>();
            Project = project;
            var employeeModels = employees.Where(x => (!project.CurrentEmployees.Contains(x)));

            foreach (var e in employeeModels)
            {
                Unemployed.Add(new ExtendedEmployeeViewModel(e));
            }
            foreach (var e in project.CurrentEmployees)
            {
                Employed.Add(new ExtendedEmployeeViewModel(e));
            }
        }

    }
}