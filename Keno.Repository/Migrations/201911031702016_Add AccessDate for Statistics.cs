namespace Keno.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccessDateforStatistics : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Statistics", "AccessDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Statistics", "AccessDate");
        }
    }
}
