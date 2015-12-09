using System.ComponentModel.DataAnnotations;
using App.Models;

namespace App.Validation
{
    public class EndDateValidationAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object project, ValidationContext validationContext)
        {

            var projectViewModel = validationContext.ObjectInstance as ProjectViewModel;
            var projectModel = projectViewModel.AsProjectModel();

            return isDateValid(projectModel)
                ? ValidationResult.Success
                : new ValidationResult("Are you a timetraveller? Check if end date is valid");
        }

        public static bool isDateValid(ProjectModel project)
        {
            return (project.EndDate == null || !(project.StartDate > project.EndDate));
        }
    }
}