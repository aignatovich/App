using App.Models;
using System.ComponentModel.DataAnnotations;

namespace App.Validation
{
    public class EndDateValidationAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object project, ValidationContext validationContext)
        {

            ProjectViewModel projectViewModel = validationContext.ObjectInstance as ProjectViewModel;
            ProjectModel projectModel = projectViewModel.AsProjectModel();

            if (isDateValid(projectModel))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Are you a timetraveller? Check if end date is valid");
        }

        public static bool isDateValid(ProjectModel project)
        {
            return (project.EndDate == null || !(project.StartDate > project.EndDate));
        }
    }
}