using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Interfaces
{
    public interface IAutocompleteEmployeeService
    {
        string FormAutocompleteResponseByName(string query);

        string FormAutocompleteResponseBySurname(string query);
    }
}
