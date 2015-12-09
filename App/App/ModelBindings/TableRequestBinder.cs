using System;
using System.Linq;
using System.Web.Mvc;
using App.Models;
using App.Models.JqGridObjects;
using App.Models.ManagingTableModels;
using Newtonsoft.Json.Linq;

namespace App.ModelBindings
{
    public  class TableRequestBinder:DefaultModelBinder
    {
        private const string keySearch = "_search";
        private const string keyPage = "page";
        private const string keyRows = "rows";
        private const string keySortProperty= "sidx";
        private const string keySortingOrder = "sord";
        private const string projectId = "projectId";
        private const string filters = "filters";

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return bindingContext.ModelType == typeof(TableRequest) ? Bind(bindingContext) : base.BindModel(controllerContext, bindingContext);
        }

        private TableRequest Bind(ModelBindingContext bindingContext)
        {
            var searchValueResult = bindingContext.ValueProvider.GetValue(keySearch);
            var pageValueResult = bindingContext.ValueProvider.GetValue(keyPage);
            var rowsValueResult = bindingContext.ValueProvider.GetValue(keyRows);
            var sortProperyValueResult = bindingContext.ValueProvider.GetValue(keySortProperty);
            var sortValueResult = bindingContext.ValueProvider.GetValue(keySortingOrder);
            var filtersValueResult = bindingContext.ValueProvider.GetValue(filters);
            var projectIdValueResult = bindingContext.ValueProvider.GetValue(projectId);

            var request = new TableRequest()
            {
                IsSearch = (bool)searchValueResult.ConvertTo(typeof(bool)),
                Page = (int)pageValueResult.ConvertTo(typeof(int)),
                Rows = (int)rowsValueResult.ConvertTo(typeof(int)),
                SortingProperty = (string)sortProperyValueResult.ConvertTo(typeof(string)),
                SortOrder = (SortEnum)Enum.Parse(typeof(SortEnum),(string)sortValueResult.ConvertTo(typeof(string))),
                ProjectId = ToNullableInt32((string)projectIdValueResult.ConvertTo(typeof(string)))
            };

            return filtersValueResult == null ? request : GetPropertyValues(request, filtersValueResult);
        }


        private TableRequest GetPropertyValues(TableRequest request, ValueProviderResult filtersValueResult)
        {
            var filters = (string)filtersValueResult.ConvertTo(typeof(string));
            var appliedFilters = JObject.Parse(filters);
            var tokens = appliedFilters.Children();

            var Name = "";
            var Surname = "";
            var Role = Roles.All;
            int? Id = null;
            var index = 0;

            foreach (var token in tokens)
            {
                if (index != 0)
                {
                    for (var j = 0; j < token.First.Count(); j++)
                    {
                        var currentToken = token.First[j];
                        if (currentToken.Value<string>("field").Equals("Name"))
                        {
                            Name = currentToken["data"].ToString();
                        }
                        if (currentToken.Value<string>("field").Equals("Surname"))
                        {
                            Surname = currentToken["data"].ToString();
                        }
                        if (currentToken.Value<string>("field").Equals("PositionValue"))
                        {
                            Role = (Roles)(Enum.Parse(typeof(Roles), currentToken["data"].ToString()));
                        }
                        if (currentToken.Value<string>("field").Equals("Id"))
                        {
                            Id = Convert.ToInt32(currentToken["data"].ToString());
                        }
                    }
                }
                index++;
            }

            request.Name = Name;
            request.Surname = Surname;
            request.Role = Role;
            request.Id = Id;

            return request;
        }

        private int? ToNullableInt32(string toParse)
        {
            int i;
            if (Int32.TryParse(toParse, out i)) return i;
            return null;
        }
    }
}