using App.DAL;
using App.Models;
using App.Service;
using App.Service.Interfaces;
using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static App.Util.AutofacConfig;

namespace App.Validation
{
    public class ExistanceProjectValidationAttribute:ValidationAttribute
    {
        private IProjectService projectService;
        private IProjectDAO projectDataAccessObject;

        protected override ValidationResult IsValid(object project, ValidationContext validationContext)
        {
            projectService = Container.Resolve<IProjectService>();
            projectDataAccessObject = Container.Resolve<IProjectDAO>();
            ProjectViewModel projectViewModel = validationContext.ObjectInstance as ProjectViewModel;
            ProjectModel projectModel = projectViewModel.AsProjectModel();

            if (!projectDataAccessObject.Exists(projectModel))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Project already exists");
        }
    }
}