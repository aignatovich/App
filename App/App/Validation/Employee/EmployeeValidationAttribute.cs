using App.DAL;
using App.Models;
using Autofac;
using System.ComponentModel.DataAnnotations;
using static App.Util.AutofacConfig;

namespace App.Validation
{
    public class EmployeeValidationAttribute: ValidationAttribute
    {
        private IEmployeeDAO employeeDataAccessObject;

        protected override  ValidationResult IsValid(object value,  ValidationContext validationContext)
        {
            employeeDataAccessObject = Container.Resolve<IEmployeeDAO>();
            if (!employeeDataAccessObject.Exists((validationContext.ObjectInstance as EmployeeViewModel).AsEmployeeModel()))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Oops, something went wrong, seems this employee has already been added");
        }
       
    }
}