namespace App.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CodeFirst.DatabaseModelContainer>
    {
        PositionsService positionsService = new PositionsService();
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
            ContextKey = "CodeFirst.DatabaseModelContainer";
        }

        protected override void Seed(CodeFirst.DatabaseModelContainer context)
        {
            for (int i = 1; i < 300; i++)
            {
                context.EmployeeSet.Add(new EmployeeModel()
                {
                    Name = "qwe" + i.ToString(),
                    Surname = "zxc" + i.ToString(),
                    Position = positionsService.GetValue(i % 5),
                    ActualProjects = new List<ProjectModel>(),
                    AbsenceList = new List<ManagingDateModel>()
                });
            }
        }
    }
}
