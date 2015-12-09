using System.Collections.Generic;

namespace App.Models.AutocompleteQueryModel
{
    public class AutocompleteQuery
    {
        public string query { get; set; }

        public List<string> suggestions { get; set; }
    }
}