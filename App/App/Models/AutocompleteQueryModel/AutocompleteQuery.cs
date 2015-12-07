using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.AutocompleteQueryModel
{
    public class AutocompleteQuery
    {
        public string query { get; set; }

        public List<string> suggestions
        {
            get; set;
        }
    }
}