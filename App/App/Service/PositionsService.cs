using App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Models
{
    public class PositionsService:IPositionsService
    {
        public List<SelectListItem> GetRoles()
        {
            IEnumerable<String> RoleList = GetNames();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (String role in RoleList)
            {
                items.Add(new SelectListItem { Text = role, Value = ((int)Enum.Parse(typeof(Roles), role)).ToString() });
            }

            return items;
        }

        public Roles GetValue(int value)
        {
            Roles roles = (Roles)value;
            return roles;
        }

        public string GetStringValue(int value)
        {
            Roles roles = (Roles)value;
            return roles.ToString();
        }

        public IEnumerable<String> GetNames()
        {
            IEnumerable<String> RoleList = Enum.GetNames(typeof(Roles)).ToList();
            return RoleList;
        }
        public Array GetValues()
        {
            Array RoleList = Enum.GetValues(typeof(Roles));
            return RoleList;
        }

    }
}