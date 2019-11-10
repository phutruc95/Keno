namespace Keno.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatesalecode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SaleCodes",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 10),
                        Percent = c.Single(nullable: false),
                        OverduedDate = c.DateTime(nullable: false),
                        Username = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
            AddColumn("dbo.Offers", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Offers", "Image");
            DropTable("dbo.SaleCodes");
        }
    }
}
