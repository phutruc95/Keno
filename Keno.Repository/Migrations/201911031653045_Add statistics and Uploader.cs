namespace Keno.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddstatisticsandUploader : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        Accessions = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Products", "UploaderID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "UploaderID");
            DropTable("dbo.Statistics");
        }
    }
}
