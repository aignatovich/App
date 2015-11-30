using App.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class ProjectModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public virtual ICollection<EmployeeModel> CurrentEmployees { get; set; }


        public ProjectModel()
        {
            CurrentEmployees = new List<EmployeeModel>();
        }
    }
}