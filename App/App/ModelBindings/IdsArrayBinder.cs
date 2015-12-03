using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.ModelBinding
{
    public class IdsArrayBinder : DefaultModelBinder
    {
        private const string key = "ids";
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(IEnumerable<Int32>))
            {
           
                return GetIdsAsList(bindingContext, key);
            }
            else
            {
                return base.BindModel(controllerContext, bindingContext);
            }
        }

        private ICollection<Int32> GetIdsAsList(ModelBindingContext bindingContext, string key)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(key);
            string[] ids = ((string)valueResult.ConvertTo(typeof(string))).Trim().Split(' ');

            ICollection<Int32> list = new List<int>();
            foreach (string id in ids)
            {
                int tmp;
                if (int.TryParse(id, out tmp))
                {
                    list.Add(tmp);
                }
            }
            return list;
        }

    }
}