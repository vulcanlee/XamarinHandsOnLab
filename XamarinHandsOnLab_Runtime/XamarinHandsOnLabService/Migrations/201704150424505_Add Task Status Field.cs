namespace XamarinHandsOnLabService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskStatusField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTasks", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTasks", "Status");
        }
    }
}
