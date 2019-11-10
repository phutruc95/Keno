namespace Keno.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addattendancelist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Coins", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.AspNetUsers", "ShopID");
            DropColumn("dbo.AspNetUsers", "SaleCodes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "SaleCodes", c => c.String());
            AddColumn("dbo.AspNetUsers", "ShopID", c => c.Int());
            DropColumn("dbo.AspNetUsers", "Coins");
        }
    }
}
