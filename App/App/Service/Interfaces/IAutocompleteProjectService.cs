using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Service.Interfaces
{
    public interface IAutocompleteProjectService
    {
        string FormAutocompleteResponse(string query);
    }
}