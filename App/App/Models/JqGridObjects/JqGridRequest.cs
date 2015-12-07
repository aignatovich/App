using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.JqGridObjects
{
    public class JqGridRequest
    {
        public bool IsSearch { get; set; }

        public int Page { get; set; }

        public int Rows { get; set; }

        public string SortingProperty { get; set; }

        public string SortOrder { get; set; }

        public string Filters { get; set; } //parse

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Role { get; set; }

        public int? Id { get; set; }

        public int ProjectId { get; set; }

        public JqGridRequest()
        {
            Name = "";
            Surname = "";
            Role = "";
            Id = null;
        }
    }
}