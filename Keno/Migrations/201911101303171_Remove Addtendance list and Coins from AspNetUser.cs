namespace Keno.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAddtendancelistandCoinsfromAspNetUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Coins");
            DropColumn("dbo.AspNetUsers", "AttendanceList");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "AttendanceList", c => c.String(maxLength: 7));
            AddColumn("dbo.AspNetUsers", "Coins", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
