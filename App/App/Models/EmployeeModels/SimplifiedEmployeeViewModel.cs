using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.EmployeeModels
{
    public class SimplifiedEmployeeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PositionValue { get; set; }


        public static SimplifiedEmployeeViewModel Create(EmployeeModel employee)
        {
            return new SimplifiedEmployeeViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                PositionValue = Enum.GetName(typeof(Roles), employee.Position)
            };
        }

        public static SimplifiedEmployeeViewModel Create(EmployeeViewModel employee)
        {
            return new SimplifiedEmployeeViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                PositionValue = Enum.GetName(typeof(Roles), employee.Position)
            };
        }



    }
}