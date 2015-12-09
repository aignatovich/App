using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using App.Validation;

namespace App.Models
{
    [EmployeeValidation]
    public class EmployeeViewModel : IViewModel<EmployeeViewModel>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [EmailAddress]
        public string Email { get; set; }

        public Roles Position { get; set; }


        public  ICollection<ProjectViewModel> ActualProjects { get; set; }

        public ICollection<ManagingDateModel> AbsenceList { get; set; }

        public EmployeeViewModel()
        {
            ActualProjects = new List<ProjectViewModel>();
            AbsenceList = new List<ManagingDateModel>();
        }

        public EmployeeViewModel(EmployeeModel employee)
        {
            Id = employee.Id;
            Name = employee.Name;
            Surname = employee.Surname;
            Position = employee.Position;
            Email = employee.Email;
            AbsenceList = new List<ManagingDateModel>(employee.AbsenceList);

            foreach (ProjectModel project in employee.ActualProjects)
            {
                //ActualProjects.Add(new ProjectViewModel(project));
            }

        }

        public EmployeeModel AsEmployeeModel()
        {
            var toTransfer = new EmployeeModel();
            toTransfer.Id = this.Id;
            toTransfer.Name = this.Name;
            toTransfer.Surname = this.Surname;
            toTransfer.Position = this.Position;
            toTransfer.Email = this.Email;
            toTransfer.AbsenceList = new List<ManagingDateModel>(this.AbsenceList);
            ICollection<ProjectModel> projectList = this.ActualProjects.Select(project => project.AsProjectModel()).ToList();
            toTransfer.ActualProjects = new List<ProjectModel>(projectList);
            return toTransfer;
        }

        public EmployeeViewModel ConvertToModel()
        {
            throw new NotImplementedException();
        }
    }
}
