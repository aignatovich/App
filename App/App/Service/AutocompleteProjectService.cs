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
    public class AutocompleteProjectService:IAutocompleteProjectService
    {
        private IProjectDAO projectDataAccessObject;

        public AutocompleteProjectService(IProjectDAO projectDataAccessObject)
        {
            this.projectDataAccessObject = projectDataAccessObject;
        }

        public string FormAutocompleteResponse(string query)
        {
            IEnumerable<ProjectModel> projects = projectDataAccessObject.Search(query);
            List<string> suggestions = new List<string>();

            foreach (ProjectModel project in projects)
            {
                suggestions.Add(project.Name);
            }

            AutocompleteQuery queryModel = new AutocompleteQuery() { query = query, suggestions = suggestions };
            return JsonConvert.SerializeObject(queryModel);
        }

    }
}