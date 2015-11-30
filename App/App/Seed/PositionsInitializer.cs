using App.Models;
using CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace App.Seed
{
    public class RolesInitializer : DropCreateDatabaseAlways<DatabaseModelContainer>
    {
        protected override void Seed(DatabaseModelContainer context)
        {
            Positions pos = new Positions();
            IList<StoredRole> rolesToSeed = new List<StoredRole>();
            IEnumerable<String> roleNameList = pos.GetNames();
            Array roleValues = pos.GetValues();

            foreach (string roleName in roleNameList)
            {
                StoredRole roleToAdd = new StoredRole();
                roleToAdd.RoleValue = roleName;
            }

            foreach ( var roleValue in roleValues)
            {
                StoredRole roleNameToAdd = new StoredRole();
                roleNameToAdd.RoleValue = roleValue.ToString();
            }

            foreach (StoredRole role in rolesToSeed)
            {
               
            }

            base.Seed(context);
        }
    }
}