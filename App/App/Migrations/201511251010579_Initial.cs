namespace App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Position = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ManagingDateModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Reason = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        EmployeeModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeModels", t => t.EmployeeModel_Id)
                .Index(t => t.EmployeeModel_Id);
            
            CreateTable(
                "dbo.ProjectModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectModelEmployeeModels",
                c => new
                    {
                        ProjectModel_Id = c.Int(nullable: false),
                        EmployeeModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectModel_Id, t.EmployeeModel_Id })
                .ForeignKey("dbo.ProjectModels", t => t.ProjectModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.EmployeeModels", t => t.EmployeeModel_Id, cascadeDelete: true)
                .Index(t => t.ProjectModel_Id)
                .Index(t => t.EmployeeModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectModelEmployeeModels", "EmployeeModel_Id", "dbo.EmployeeModels");
            DropForeignKey("dbo.ProjectModelEmployeeModels", "ProjectModel_Id", "dbo.ProjectModels");
            DropForeignKey("dbo.ManagingDateModels", "EmployeeModel_Id", "dbo.EmployeeModels");
            DropIndex("dbo.ProjectModelEmployeeModels", new[] { "EmployeeModel_Id" });
            DropIndex("dbo.ProjectModelEmployeeModels", new[] { "ProjectModel_Id" });
            DropIndex("dbo.ManagingDateModels", new[] { "EmployeeModel_Id" });
            DropTable("dbo.ProjectModelEmployeeModels");
            DropTable("dbo.ProjectModels");
            DropTable("dbo.ManagingDateModels");
            DropTable("dbo.EmployeeModels");
        }
    }
}
