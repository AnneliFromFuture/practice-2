namespace MorePractice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaaaaaa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Receptions", "RecTime", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Receptions", "RecTime", c => c.DateTime(nullable: false));
        }
    }
}
