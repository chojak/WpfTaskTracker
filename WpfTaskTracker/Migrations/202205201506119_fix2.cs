namespace WpfTaskTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subtasks", "IsCompleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tasks", "IsCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "IsCompleted");
            DropColumn("dbo.Subtasks", "IsCompleted");
        }
    }
}
