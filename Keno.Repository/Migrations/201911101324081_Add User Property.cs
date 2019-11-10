namespace Keno.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserProperty : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProperties",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 250),
                        AttendanceList = c.String(maxLength: 7),
                        Coins = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Username);
            
            AlterColumn("dbo.SaleCodes", "Username", c => c.String(maxLength: 250));
            CreateIndex("dbo.SaleCodes", "Username");
            AddForeignKey("dbo.SaleCodes", "Username", "dbo.UserProperties", "Username");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleCodes", "Username", "dbo.UserProperties");
            DropIndex("dbo.SaleCodes", new[] { "Username" });
            AlterColumn("dbo.SaleCodes", "Username", c => c.Int(nullable: false));
            DropTable("dbo.UserProperties");
        }
    }
}
