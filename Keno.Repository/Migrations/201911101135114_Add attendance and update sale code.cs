namespace Keno.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addattendanceandupdatesalecode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offers", "CoinsConsumed", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Offers", "CoinsConsumed");
        }
    }
}
