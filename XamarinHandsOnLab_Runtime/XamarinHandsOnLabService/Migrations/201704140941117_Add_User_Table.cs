namespace XamarinHandsOnLabService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_User_Table : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
