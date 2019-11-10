namespace Keno.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatesalecodesasafieldofuserproperty : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SaleCodes", "Username", "dbo.UserProperties");
            DropIndex("dbo.SaleCodes", new[] { "Username" });
            CreateIndex("dbo.SaleCodes", "Username");
            AddForeignKey("dbo.SaleCodes", "Username", "dbo.UserProperties", "Username");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleCodes", "Username", "dbo.UserProperties");
            DropIndex("dbo.SaleCodes", new[] { "Username" });
            CreateIndex("dbo.SaleCodes", "Username");
            AddForeignKey("dbo.SaleCodes", "Username", "dbo.UserProperties", "Username");
        }
    }
}
