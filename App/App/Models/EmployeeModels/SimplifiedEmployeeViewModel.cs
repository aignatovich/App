﻿using System;

namespace App.Models.EmployeeModels
{
    public class SimplifiedEmployeeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Position { get; set; }


        public SimplifiedEmployeeViewModel(EmployeeModel employee)
        {
            Id = employee.Id;
            Name = employee.Name;
            Surname = employee.Surname;
            Position = Enum.GetName(typeof(Roles), employee.Position);
        }

        public  SimplifiedEmployeeViewModel (EmployeeViewModel employee)
        {
            Id = employee.Id;
            Name = employee.Name;
            Surname = employee.Surname;
            Position = Enum.GetName(typeof(Roles), employee.Position);
        }
    }
}