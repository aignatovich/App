using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using App.Service.Interfaces;

namespace App.Models
{
    public class PositionsService:IPositionsService
    {
        public List<SelectListItem> GetRoles()
        {
            var RoleList = GetNames();
            return RoleList.Select(role => new SelectListItem {Text = role, Value = ((int) Enum.Parse(typeof (Roles), role)).ToString()}).ToList();
        }

        public Roles GetValue(int value)
        {
            var roles = (Roles)value;
            return roles;
        }

        public string GetStringValue(int value)
        {
            var roles = (Roles)value;
            return roles.ToString();
        }

        public IEnumerable<String> GetNames()
        {
            var RoleList = Enum.GetNames(typeof(Roles)).ToList();
            return RoleList;
        }
        public Array GetValues()
        {
            var RoleList = Enum.GetValues(typeof(Roles));
            return RoleList;
        }

    }
}