namespace WpfTaskTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Subtasks",
                c => new
                    {
                        SubtaskId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TaskId = c.Int(),
                    })
                .PrimaryKey(t => t.SubtaskId)
                .ForeignKey("dbo.Tasks", t => t.TaskId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryId = c.Int(),
                        Urgency = c.Int(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subtasks", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.Tasks", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Tasks", new[] { "CategoryId" });
            DropIndex("dbo.Subtasks", new[] { "TaskId" });
            DropTable("dbo.Tasks");
            DropTable("dbo.Subtasks");
            DropTable("dbo.Categories");
        }
    }
}
