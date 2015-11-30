using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    class EmployeeComparer : IEqualityComparer<EmployeeViewModel>
    {
        public bool Equals(EmployeeViewModel x, EmployeeViewModel y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(EmployeeViewModel obj)
        {
            return obj.Id;
        }

    }

}