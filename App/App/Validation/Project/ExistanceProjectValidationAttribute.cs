using System.ComponentModel.DataAnnotations;
using App.DAL;
using App.Models;
using App.Service.Interfaces;
using Autofac;
using static App.Util.AutofacConfig;

namespace App.Validation
{
    public class ExistanceProjectValidationAttribute:ValidationAttribute
    {
        private IProjectDAO projectDataAccessObject;

        protected override ValidationResult IsValid(object project, ValidationContext validationContext)
        {
            projectDataAccessObject = Container.Resolve<IProjectDAO>();
            var projectViewModel = validationContext.ObjectInstance as ProjectViewModel;
            var projectModel = projectViewModel.AsProjectModel();

            return !projectDataAccessObject.Exists(projectModel)
                ? ValidationResult.Success
                : new ValidationResult("Project already exists");
        }
    }
}