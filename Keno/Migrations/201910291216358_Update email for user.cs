namespace Keno.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updateemailforuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Email", c => c.String());
            AddColumn("dbo.AspNetUsers", "ShopID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "SaleCodes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SaleCodes");
            DropColumn("dbo.AspNetUsers", "ShopID");
            DropColumn("dbo.AspNetUsers", "Email");
        }
    }
}
