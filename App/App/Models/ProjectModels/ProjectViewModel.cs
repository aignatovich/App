using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using App.Validation;

namespace App.Models
{
    [ExistanceProjectValidation]
    public class ProjectViewModel : IViewModel<ProjectModel>
    {
        private const string EMPTY_DATE_VALUE_PLACEHOLDER = "...";

        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public string StartDate { get; set; }

        [EndDateValidation]
        public string EndDate { get; set; }

        public ICollection<EmployeeViewModel> CurrentEmployees { get; set; }

        public ProjectViewModel()
        {
            CurrentEmployees = new List<EmployeeViewModel>();
        }

        public ProjectViewModel(ProjectModel project)
        {
            Id = project.Id;
            Name = project.Name;
            StartDate = project.StartDate.ToShortDateString();
            CurrentEmployees = new List<EmployeeViewModel>();

            foreach (var e in project.CurrentEmployees)
            {
                CurrentEmployees.Add(new EmployeeViewModel(e));
            }

            EndDate = project.EndDate?.ToShortDateString() ?? EMPTY_DATE_VALUE_PLACEHOLDER;
        }

        public ProjectModel AsProjectModel()
        {
            var project = new ProjectModel();
            project.Id = this.Id;
            project.Name = this.Name;
            var temporaryDate = new DateTime();
            DateTime.TryParse(this.StartDate, out temporaryDate);
            project.StartDate = temporaryDate;

            foreach (var e in this.CurrentEmployees)
            {
                project.CurrentEmployees.Add(e.AsEmployeeModel());
            }

            if (EndDate != null)
            {
                DateTime.TryParse(this.EndDate, out temporaryDate);
                project.EndDate = temporaryDate;
            }

            return project;
        }

        public static ProjectViewModel Create(ProjectModel project)
        {
            return new ProjectViewModel(project);
        }

        public static ProjectViewModel Create()
        {
            return new ProjectViewModel();
        }

        public ProjectModel ConvertToModel()
        {
            throw new NotImplementedException();
        }

    }
}