using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class StoredRole
    {
        public int RoleId;
        public string RoleValue;
        //public virtual ICollection<Employee> Employees { get; set; }

    }
}