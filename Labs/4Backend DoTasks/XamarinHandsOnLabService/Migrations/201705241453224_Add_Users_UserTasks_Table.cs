namespace XamarinHandsOnLabService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Users_UserTasks_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Account = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Department = c.String(),
                        PhotoUrl = c.String(),
                        ManagerId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserTasks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Account = c.String(),
                        TaskDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        CheckinId = c.String(),
                        Checkin_Latitude = c.Double(nullable: false),
                        Checkin_Longitude = c.Double(nullable: false),
                        CheckinDatetime = c.DateTime(nullable: false),
                        Condition1_Ttile = c.String(),
                        Condition1_Result = c.String(),
                        Condition2_Ttile = c.String(),
                        Condition2_Result = c.String(),
                        Condition3_Ttile = c.String(),
                        Condition3_Result = c.String(),
                        PhotoURL = c.String(),
                        Reported = c.Boolean(nullable: false),
                        ReportedDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserTasks");
            DropTable("dbo.Users");
        }
    }
}
