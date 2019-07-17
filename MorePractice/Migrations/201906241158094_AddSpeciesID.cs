namespace MorePractice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpeciesID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pets", "Species_ID", "dbo.Species");
            DropIndex("dbo.Pets", new[] { "Species_ID" });
            DropColumn("dbo.Pets", "ID_Species");
            RenameColumn(table: "dbo.Pets", name: "Species_ID", newName: "ID_Species");
            AlterColumn("dbo.Pets", "ID_Species", c => c.Guid(nullable: false));
            CreateIndex("dbo.Pets", "ID_Species");
            AddForeignKey("dbo.Pets", "ID_Species", "dbo.Species", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pets", "ID_Species", "dbo.Species");
            DropIndex("dbo.Pets", new[] { "ID_Species" });
            AlterColumn("dbo.Pets", "ID_Species", c => c.Guid());
            RenameColumn(table: "dbo.Pets", name: "ID_Species", newName: "Species_ID");
            AddColumn("dbo.Pets", "ID_Species", c => c.Guid(nullable: false));
            CreateIndex("dbo.Pets", "Species_ID");
            AddForeignKey("dbo.Pets", "Species_ID", "dbo.Species", "ID");
        }
    }
}
