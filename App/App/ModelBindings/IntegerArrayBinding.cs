using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Binding
{
    public class IntegerArrayFilterAttribute : IModelBinder
    {
        public string ParameterName { get; set; }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;
            string[] idsSequence = request.Form.Get("ids").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string[] ids = idsSequence[0].Trim().Split(' ');
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