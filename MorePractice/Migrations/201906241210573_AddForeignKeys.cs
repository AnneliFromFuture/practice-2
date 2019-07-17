namespace MorePractice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceDoctors", "Doctor_ID", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "Specializ_ID", "dbo.Specializs");
            DropForeignKey("dbo.Doctors", "VetClinic_ID", "dbo.VetClinics");
            DropForeignKey("dbo.Species", "Kind_ID", "dbo.Kinds");
            DropForeignKey("dbo.ServiceDoctors", "Service_ID", "dbo.Services");
            DropIndex("dbo.Doctors", new[] { "Specializ_ID" });
            DropIndex("dbo.Doctors", new[] { "VetClinic_ID" });
            DropIndex("dbo.Species", new[] { "Kind_ID" });
            DropIndex("dbo.ServiceDoctors", new[] { "Doctor_ID" });
            DropIndex("dbo.ServiceDoctors", new[] { "Service_ID" });
            DropColumn("dbo.Doctors", "ID_Special");
            DropColumn("dbo.Doctors", "ID_Clinic");
            DropColumn("dbo.Receptions", "ID_Doctor");
            DropColumn("dbo.Receptions", "ID_Pet");
            DropColumn("dbo.Receptions", "ID_Service");
            DropColumn("dbo.Species", "ID_Kind");
            DropColumn("dbo.ServiceDoctors", "ID_Doctor");
            DropColumn("dbo.ServiceDoctors", "ID_Service");
            RenameColumn(table: "dbo.Receptions", name: "Doctor_ID", newName: "ID_Doctor");
            RenameColumn(table: "dbo.ServiceDoctors", name: "Doctor_ID", newName: "ID_Doctor");
            RenameColumn(table: "dbo.Doctors", name: "Specializ_ID", newName: "ID_Special");
            RenameColumn(table: "dbo.Doctors", name: "VetClinic_ID", newName: "ID_Clinic");
            RenameColumn(table: "dbo.Receptions", name: "Pet_ID", newName: "ID_Pet");
            RenameColumn(table: "dbo.Receptions", name: "Service_ID", newName: "ID_Service");
            RenameColumn(table: "dbo.Species", name: "Kind_ID", newName: "ID_Kind");
            RenameColumn(table: "dbo.ServiceDoctors", name: "Service_ID", newName: "ID_Service");
            RenameIndex(table: "dbo.Receptions", name: "IX_Pet_ID", newName: "IX_ID_Pet");
            RenameIndex(table: "dbo.Receptions", name: "IX_Service_ID", newName: "IX_ID_Service");
            RenameIndex(table: "dbo.Receptions", name: "IX_Doctor_ID", newName: "IX_ID_Doctor");
            AlterColumn("dbo.Doctors", "ID_Special", c => c.Guid(nullable: false));
            AlterColumn("dbo.Doctors", "ID_Clinic", c => c.Guid(nullable: false));
            AlterColumn("dbo.Species", "ID_Kind", c => c.Guid(nullable: false));
            AlterColumn("dbo.ServiceDoctors", "ID_Doctor", c => c.Guid(nullable: false));
            AlterColumn("dbo.ServiceDoctors", "ID_Service", c => c.Guid(nullable: false));
            CreateIndex("dbo.Doctors", "ID_Clinic");
            CreateIndex("dbo.Doctors", "ID_Special");
            CreateIndex("dbo.Species", "ID_Kind");
            CreateIndex("dbo.ServiceDoctors", "ID_Service");
            CreateIndex("dbo.ServiceDoctors", "ID_Doctor");
            AddForeignKey("dbo.ServiceDoctors", "ID_Doctor", "dbo.Doctors", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Doctors", "ID_Special", "dbo.Specializs", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Doctors", "ID_Clinic", "dbo.VetClinics", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Species", "ID_Kind", "dbo.Kinds", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ServiceDoctors", "ID_Service", "dbo.Services", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceDoctors", "ID_Service", "dbo.Services");
            DropForeignKey("dbo.Species", "ID_Kind", "dbo.Kinds");
            DropForeignKey("dbo.Doctors", "ID_Clinic", "dbo.VetClinics");
            DropForeignKey("dbo.Doctors", "ID_Special", "dbo.Specializs");
            DropForeignKey("dbo.ServiceDoctors", "ID_Doctor", "dbo.Doctors");
            DropIndex("dbo.ServiceDoctors", new[] { "ID_Doctor" });
            DropIndex("dbo.ServiceDoctors", new[] { "ID_Service" });
            DropIndex("dbo.Species", new[] { "ID_Kind" });
            DropIndex("dbo.Doctors", new[] { "ID_Special" });
            DropIndex("dbo.Doctors", new[] { "ID_Clinic" });
            AlterColumn("dbo.ServiceDoctors", "ID_Service", c => c.Guid());
            AlterColumn("dbo.ServiceDoctors", "ID_Doctor", c => c.Guid());
            AlterColumn("dbo.Species", "ID_Kind", c => c.Guid());
            AlterColumn("dbo.Doctors", "ID_Clinic", c => c.Guid());
            AlterColumn("dbo.Doctors", "ID_Special", c => c.Guid());
            RenameIndex(table: "dbo.Receptions", name: "IX_ID_Doctor", newName: "IX_Doctor_ID");
            RenameIndex(table: "dbo.Receptions", name: "IX_ID_Service", newName: "IX_Service_ID");
            RenameIndex(table: "dbo.Receptions", name: "IX_ID_Pet", newName: "IX_Pet_ID");
            RenameColumn(table: "dbo.ServiceDoctors", name: "ID_Service", newName: "Service_ID");
            RenameColumn(table: "dbo.Species", name: "ID_Kind", newName: "Kind_ID");
            RenameColumn(table: "dbo.Receptions", name: "ID_Service", newName: "Service_ID");
            RenameColumn(table: "dbo.Receptions", name: "ID_Pet", newName: "Pet_ID");
            RenameColumn(table: "dbo.Doctors", name: "ID_Clinic", newName: "VetClinic_ID");
            RenameColumn(table: "dbo.Doctors", name: "ID_Special", newName: "Specializ_ID");
            RenameColumn(table: "dbo.ServiceDoctors", name: "ID_Doctor", newName: "Doctor_ID");
            RenameColumn(table: "dbo.Receptions", name: "ID_Doctor", newName: "Doctor_ID");
            AddColumn("dbo.ServiceDoctors", "ID_Service", c => c.Guid(nullable: false));
            AddColumn("dbo.ServiceDoctors", "ID_Doctor", c => c.Guid(nullable: false));
            AddColumn("dbo.Species", "ID_Kind", c => c.Guid(nullable: false));
            AddColumn("dbo.Receptions", "ID_Service", c => c.Guid(nullable: false));
            AddColumn("dbo.Receptions", "ID_Pet", c => c.Guid(nullable: false));
            AddColumn("dbo.Receptions", "ID_Doctor", c => c.Guid(nullable: false));
            AddColumn("dbo.Doctors", "ID_Clinic", c => c.Guid(nullable: false));
            AddColumn("dbo.Doctors", "ID_Special", c => c.Guid(nullable: false));
            CreateIndex("dbo.ServiceDoctors", "Service_ID");
            CreateIndex("dbo.ServiceDoctors", "Doctor_ID");
            CreateIndex("dbo.Species", "Kind_ID");
            CreateIndex("dbo.Doctors", "VetClinic_ID");
            CreateIndex("dbo.Doctors", "Specializ_ID");
            AddForeignKey("dbo.ServiceDoctors", "Service_ID", "dbo.Services", "ID");
            AddForeignKey("dbo.Species", "Kind_ID", "dbo.Kinds", "ID");
            AddForeignKey("dbo.Doctors", "VetClinic_ID", "dbo.VetClinics", "ID");
            AddForeignKey("dbo.Doctors", "Specializ_ID", "dbo.Specializs", "ID");
            AddForeignKey("dbo.ServiceDoctors", "Doctor_ID", "dbo.Doctors", "ID");
        }
    }
}
