using System.Collections.Generic;

namespace App.Models.AutocompleteQueryModel
{
    public class AutocompleteQuery
    {
        public string Query { get; set; }

        public List<string> Suggestions { get; set; }
    }
}