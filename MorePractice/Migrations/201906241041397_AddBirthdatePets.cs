namespace MorePractice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBirthdatePets : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pets", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pets", "Name", c => c.String());
        }
    }
}
