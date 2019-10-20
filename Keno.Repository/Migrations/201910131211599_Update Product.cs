namespace Keno.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Advertisements", "Product_ID", "dbo.Products");
            DropIndex("dbo.Advertisements", new[] { "Product_ID" });
            DropColumn("dbo.Advertisements", "ProductID");
            RenameColumn(table: "dbo.Advertisements", name: "Product_ID", newName: "ProductID");
            AlterColumn("dbo.Advertisements", "ProductID", c => c.Int(nullable: false));
            AlterColumn("dbo.Advertisements", "ProductID", c => c.Int(nullable: false));
            CreateIndex("dbo.Advertisements", "ProductID");
            AddForeignKey("dbo.Advertisements", "ProductID", "dbo.Products", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Advertisements", "ProductID", "dbo.Products");
            DropIndex("dbo.Advertisements", new[] { "ProductID" });
            AlterColumn("dbo.Advertisements", "ProductID", c => c.Int());
            AlterColumn("dbo.Advertisements", "ProductID", c => c.String());
            RenameColumn(table: "dbo.Advertisements", name: "ProductID", newName: "Product_ID");
            AddColumn("dbo.Advertisements", "ProductID", c => c.String());
            CreateIndex("dbo.Advertisements", "Product_ID");
            AddForeignKey("dbo.Advertisements", "Product_ID", "dbo.Products", "ID");
        }
    }
}
