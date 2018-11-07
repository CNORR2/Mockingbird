namespace MockingBird.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DiskCheckers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ServerName = c.String(nullable: false),
                        DriveLetter = c.String(nullable: false),
                        LowDiskPercentage = c.Int(nullable: false),
                        PollTime = c.Int(nullable: false),
                        AdminEmailAlert = c.Boolean(nullable: false),
                        AlertSubscribers = c.Boolean(nullable: false),
                        SubcriptionEmailAddresses = c.String(),
                        Disable = c.Boolean(nullable: false),
                        ShowOnDash = c.Boolean(nullable: false),
                        LastRan = c.DateTime(nullable: false),
                        IgnorePollTimeOnNextRun = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DiskCheckers");
            DropTable("dbo.SchedulledTaskStatuses");
        }
    }
}
