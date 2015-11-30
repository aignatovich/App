using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static App.DAL.EmployeeDAO;

namespace App.Models
{
        public class IfEmployeeIsUnique : RequiredAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext context)
            {
                Employee employee = (Employee)context.ObjectInstance;

                if (!Exists(employee))
                {
                        return ValidationResult.Success;                    
                }
                return new ValidationResult(ErrorMessage);
            }
        }
  
}