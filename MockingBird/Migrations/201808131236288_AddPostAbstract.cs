namespace MockingBird.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostAbstract : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiskCheckers", "DriveSize", c => c.String());
            AddColumn("dbo.DiskCheckers", "AvailableSpace", c => c.String());
            Sql("UPDATE dbo.DiskCheckers SET DriveSize = LEFT(LastRan, 100) WHERE DriveSize IS NULL");
            Sql("UPDATE dbo.DiskCheckers SET AvailableSpace = LEFT(LastRan, 100) WHERE AvailableSpace IS NULL");
        }
        
        public override void Down()
        {
            DropColumn("dbo.DiskCheckers", "AvailableSpace");
            DropColumn("dbo.DiskCheckers", "DriveSize");
            DropColumn("dbo.SchedulledTaskStatuses", "MockingBirdStatus");
        }
    }
}
