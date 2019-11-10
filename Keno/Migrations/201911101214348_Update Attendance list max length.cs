namespace Keno.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAttendancelistmaxlength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "AttendanceList", c => c.String(maxLength: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "AttendanceList", c => c.String());
        }
    }
}
