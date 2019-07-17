namespace MorePractice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reception : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Receptions", "Pet_ID", "dbo.Pets");
            DropForeignKey("dbo.Receptions", "Doctor_ID", "dbo.Doctors");
            DropForeignKey("dbo.Receptions", "Service_ID", "dbo.Services");
            DropIndex("dbo.Receptions", new[] { "Doctor_ID" });
            DropIndex("dbo.Receptions", new[] { "Service_ID" });
            DropIndex("dbo.Receptions", new[] { "Pet_ID" });
            AlterColumn("dbo.Receptions", "Doctor_ID", c => c.Guid(nullable: false));
            AlterColumn("dbo.Receptions", "Service_ID", c => c.Guid(nullable: false));
            AlterColumn("dbo.Receptions", "Pet_ID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Receptions", "Doctor_ID");
            CreateIndex("dbo.Receptions", "Pet_ID");
            CreateIndex("dbo.Receptions", "Service_ID");
            AddForeignKey("dbo.Receptions", "Pet_ID", "dbo.Pets", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Receptions", "Doctor_ID", "dbo.Doctors", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Receptions", "Service_ID", "dbo.Services", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Receptions", "Service_ID", "dbo.Services");
            DropForeignKey("dbo.Receptions", "Doctor_ID", "dbo.Doctors");
            DropForeignKey("dbo.Receptions", "Pet_ID", "dbo.Pets");
            DropIndex("dbo.Receptions", new[] { "Service_ID" });
            DropIndex("dbo.Receptions", new[] { "Pet_ID" });
            DropIndex("dbo.Receptions", new[] { "Doctor_ID" });
            AlterColumn("dbo.Receptions", "Pet_ID", c => c.Guid());
            AlterColumn("dbo.Receptions", "Service_ID", c => c.Guid());
            AlterColumn("dbo.Receptions", "Doctor_ID", c => c.Guid());
            CreateIndex("dbo.Receptions", "Pet_ID");
            CreateIndex("dbo.Receptions", "Service_ID");
            CreateIndex("dbo.Receptions", "Doctor_ID");
            AddForeignKey("dbo.Receptions", "Service_ID", "dbo.Services", "ID");
            AddForeignKey("dbo.Receptions", "Doctor_ID", "dbo.Doctors", "ID");
            AddForeignKey("dbo.Receptions", "Pet_ID", "dbo.Pets", "ID");
        }
    }
}
