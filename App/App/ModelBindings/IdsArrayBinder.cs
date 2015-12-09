using System.Collections.Generic;
using System.Web.Mvc;

namespace App.ModelBinding
{
    public class IdsArrayBinder : DefaultModelBinder
    {
        private const string key = "ids";
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return bindingContext.ModelType == typeof(IEnumerable<int>) ? GetIdsAsList(bindingContext, key) : base.BindModel(controllerContext, bindingContext);
        }

        private ICollection<int> GetIdsAsList(ModelBindingContext bindingContext, string key)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(key);
            ICollection<int> list = new List<int>();

            if (valueResult != null)
            {
                var ids = ((string)valueResult.ConvertTo(typeof(string))).Trim().Split(' ');

                foreach (var id in ids)
                {
                    int tmp;
                    if (int.TryParse(id, out tmp))
                    {
                        list.Add(tmp);
                    }
                }
            }
            return list;
        }

    }
}