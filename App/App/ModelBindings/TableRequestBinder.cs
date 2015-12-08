using App.Models;
using App.Models.JqGridObjects;
using App.Models.ManagingTableModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            if (bindingContext.ModelType == typeof(TableRequest))
            {

                return Bind(bindingContext);
            }
            else
            {
                return base.BindModel(controllerContext, bindingContext);
            }
        }

        private TableRequest Bind(ModelBindingContext bindingContext)
        {
            ValueProviderResult searchValueResult = bindingContext.ValueProvider.GetValue(keySearch);
            ValueProviderResult pageValueResult = bindingContext.ValueProvider.GetValue(keyPage);
            ValueProviderResult rowsValueResult = bindingContext.ValueProvider.GetValue(keyRows);
            ValueProviderResult sortProperyValueResult = bindingContext.ValueProvider.GetValue(keySortProperty);
            ValueProviderResult sortValueResult = bindingContext.ValueProvider.GetValue(keySortingOrder);
            ValueProviderResult filtersValueResult = bindingContext.ValueProvider.GetValue(filters);
            ValueProviderResult projectIdValueResult = bindingContext.ValueProvider.GetValue(projectId);

            TableRequest request = new TableRequest()
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
            string filters = (string)filtersValueResult.ConvertTo(typeof(string));
            JObject appliedFilters = JObject.Parse(filters);
            JEnumerable<JToken> tokens = appliedFilters.Children();

            string Name = "";
            string Surname = "";
            Roles Role = Roles.All;
            int? Id = null;
            int index = 0;

            foreach (JToken token in tokens)
            {
                if (index != 0)
                {
                    for (int j = 0; j < token.First.Count(); j++)
                    {
                        JToken currentToken = token.First[j];
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