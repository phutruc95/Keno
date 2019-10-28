namespace Keno.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductAddShopandOffer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ShortDesc", c => c.String());
            AddColumn("dbo.Products", "Desc", c => c.String());
            AddColumn("dbo.Products", "Rate", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Rate");
            DropColumn("dbo.Products", "Desc");
            DropColumn("dbo.Products", "ShortDesc");
        }
    }
}
