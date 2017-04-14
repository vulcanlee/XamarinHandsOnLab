namespace XamarinHandsOnLabService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserTasksSchema : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTasks", "CheckinDatetime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTasks", "CheckinDatetime");
        }
    }
}
