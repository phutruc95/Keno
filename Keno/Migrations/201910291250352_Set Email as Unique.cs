namespace Keno.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetEmailasUnique : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String(maxLength: 250));
            CreateIndex("dbo.AspNetUsers", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Email" });
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String());
        }
    }
}
