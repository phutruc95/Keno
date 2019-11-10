namespace Keno.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updateattendancelist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AttendanceList", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AttendanceList");
        }
    }
}
