using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.EmployeeModels
{
    public class PagingQueryResult
    {
        public IEnumerable<EmployeeModel> Employees { get; set; }
        public int ResultQuantity { get; set; }
    }
}