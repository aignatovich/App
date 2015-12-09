using System.Collections.Generic;
using App.Models.EmployeeModels;
using App.Models.ManagingTableModels;

namespace App.Models.JqGridObjects
{
    public class EmployeePagedCollection
    {
        public int Page { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }

        public string SortColumn { get; set; }

        public SortEnum SortOrder { get; set; }

        public IEnumerable<SimplifiedEmployeeViewModel> Employees { get; set; }
    }
}