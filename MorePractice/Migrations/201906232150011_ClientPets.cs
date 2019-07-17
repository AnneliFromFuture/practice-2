namespace MorePractice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientPets : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Pets", new[] { "OwnerId" });
            CreateIndex("dbo.Pets", "OwnerID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Pets", new[] { "OwnerID" });
            CreateIndex("dbo.Pets", "OwnerId");
        }
    }
}
