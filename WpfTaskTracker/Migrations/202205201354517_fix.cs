namespace WpfTaskTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subtasks", "TaskId", "dbo.Tasks");
            DropIndex("dbo.Subtasks", new[] { "TaskId" });
            AlterColumn("dbo.Subtasks", "TaskId", c => c.Int(nullable: false));
            CreateIndex("dbo.Subtasks", "TaskId");
            AddForeignKey("dbo.Subtasks", "TaskId", "dbo.Tasks", "TaskId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subtasks", "TaskId", "dbo.Tasks");
            DropIndex("dbo.Subtasks", new[] { "TaskId" });
            AlterColumn("dbo.Subtasks", "TaskId", c => c.Int());
            CreateIndex("dbo.Subtasks", "TaskId");
            AddForeignKey("dbo.Subtasks", "TaskId", "dbo.Tasks", "TaskId");
        }
    }
}
