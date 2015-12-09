using System.Linq;
using App.DAL;
using App.Models.AutocompleteQueryModel;
using App.Service.Interfaces;
using Newtonsoft.Json;

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
            var projects = projectDataAccessObject.Search(query);
            var suggestions = projects.Select(project => project.Name).ToList();
            var queryModel = new AutocompleteQuery() { Query = query, Suggestions = suggestions };
            return JsonConvert.SerializeObject(queryModel);
        }

    }
}