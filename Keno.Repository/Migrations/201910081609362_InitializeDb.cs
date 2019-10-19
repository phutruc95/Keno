namespace Keno.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Advertisements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Image = c.String(),
                        Url = c.String(),
                        ProductID = c.String(),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Image = c.String(),
                        Url = c.String(),
                        Price = c.Single(nullable: false),
                        SalePrice = c.Single(nullable: false),
                        IsOnSale = c.Boolean(nullable: false),
                        ProductTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeID, cascadeDelete: true)
                .Index(t => t.ProductTypeID);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Advertisements", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductTypeID", "dbo.ProductTypes");
            DropIndex("dbo.Advertisements", new[] { "Product_ID" });
            DropIndex("dbo.Products", new[] { "ProductTypeID" });
            DropTable("dbo.ProductTypes");
            DropTable("dbo.Products");
            DropTable("dbo.Advertisements");
        }
    }
}
