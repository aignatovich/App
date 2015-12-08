using App.DAL;
using App.Models;
using App.Models.AutocompleteQueryModel;
using App.Service.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            IEnumerable<EmployeeModel> employees = employeeDataAccessObject.DirectSearch(query, null, null, Roles.All);
            List<string> suggestions = new List<string>();

            foreach (EmployeeModel employee in employees)
            {
                suggestions.Add(employee.Name);
            }

            AutocompleteQuery queryModel = new AutocompleteQuery() { query = query, suggestions = suggestions };
            return JsonConvert.SerializeObject(queryModel);
        }

        public string FormAutocompleteResponseBySurname(string query)
        {
            IEnumerable<EmployeeModel> employees = employeeDataAccessObject.DirectSearch(null, query, null, Roles.All);
            List<string> suggestions = new List<string>();

            foreach (EmployeeModel employee in employees)
            {
                suggestions.Add(employee.Surname);
            }

            AutocompleteQuery queryModel = new AutocompleteQuery() { query = query, suggestions = suggestions };
            return JsonConvert.SerializeObject(queryModel);
        }
    }
}