using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class ExtendedEmployeeViewModel
    {
        public int Id { get; set; }
        public string Employee { get; set; }

        public ExtendedEmployeeViewModel()
        {
        }

        public ExtendedEmployeeViewModel(EmployeeViewModel e)
        {
            Id = e.Id;
            Employee = String.Format("{2} - {0} {1}", e.Name, e.Surname, e.Position);            
        }
    }
}