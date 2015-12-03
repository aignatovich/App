using App.Models;
using App.Models.JqGridObjects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.ModelBindings
{
    public class JqGridRequestBinder:DefaultModelBinder
    {
        private const string keySearch = "_search";
        private const string keyPage = "page";
        private const string keyRows = "rows";
        private const string keySidx = "sidx";
        private const string keySortingOrderd = "sord";
        private const string filters = "filters";

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(JqGridRequest))
            {

                return Bind(bindingContext, keySearch, keyPage, keyRows, keySidx, keySortingOrderd, filters);
            }
            else
            {
                return base.BindModel(controllerContext, bindingContext);
            }
        }

        private JqGridRequest Bind(ModelBindingContext bindingContext, string keySearch, string keyPage, string keyRows, string keySortPropery, string keySort, string filters)
        {
            ValueProviderResult searchValueResult = bindingContext.ValueProvider.GetValue(keySearch);
            ValueProviderResult pageValueResult = bindingContext.ValueProvider.GetValue(keyPage);
            ValueProviderResult rowsValueResult = bindingContext.ValueProvider.GetValue(keyRows);
            ValueProviderResult sortProperyValueResult = bindingContext.ValueProvider.GetValue(keySortPropery);
            ValueProviderResult sortValueResult = bindingContext.ValueProvider.GetValue(keySort);
            ValueProviderResult filtersValueResult = bindingContext.ValueProvider.GetValue(filters);

            JqGridRequest request = new JqGridRequest()
            {
                IsSearch = (bool)searchValueResult.ConvertTo(typeof(bool)),
                Page = (int)pageValueResult.ConvertTo(typeof(int)),
                Rows = (int)rowsValueResult.ConvertTo(typeof(int)),
                SortingProperty = (string)sortProperyValueResult.ConvertTo(typeof(string)),
                SortOrder = (string)sortValueResult.ConvertTo(typeof(string)),
            };

            return filtersValueResult == null ? request : GetPropertyValues(request, filtersValueResult);
        }

        private JqGridRequest GetPropertyValues(JqGridRequest request, ValueProviderResult filtersValueResult)
        {
            string filters = (string)filtersValueResult.ConvertTo(typeof(string));
            JObject appliedFilters = JObject.Parse(filters);
            JEnumerable<JToken> tokens = appliedFilters.Children();

            string Name = "";
            string Surname = "";
            string Role = "";
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
                            Role = (Enum.Parse(typeof(Roles), currentToken["data"].ToString())).ToString();
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
    }
}