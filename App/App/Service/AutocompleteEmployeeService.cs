using System.Linq;
using App.DAL;
using App.Models;
using App.Models.AutocompleteQueryModel;
using App.Service.Interfaces;
using Newtonsoft.Json;

namespace App.Service
{
    public class AutocompleteEmployeeService:IAutocompleteEmployeeService
    {
        private IEmployeeDAO employeeDataAccessObject;

        public AutocompleteEmployeeService(IEmployeeDAO employeeDataAccessObject)
        {
            this.employeeDataAccessObject = employeeDataAccessObject;
        }

        public string FormAutocompleteResponseByName(string query)
        {
            var employees = employeeDataAccessObject.DirectSearch(query, null, null, Roles.All, null);
            var suggestions = employees.Select(employee => employee.Name).ToList();
            var queryModel = new AutocompleteQuery() { query = query, suggestions = suggestions };
            return JsonConvert.SerializeObject(queryModel);
        }

        public string FormAutocompleteResponseBySurname(string query)
        {
            var employees = employeeDataAccessObject.DirectSearch(null, query, null, Roles.All, null);
            var suggestions = employees.Select(employee => employee.Surname).ToList();
            var queryModel = new AutocompleteQuery() { query = query, suggestions = suggestions };
            return JsonConvert.SerializeObject(queryModel);
        }
    }
}