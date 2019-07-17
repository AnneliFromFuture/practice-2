namespace MorePractice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vet : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VetClinics", "Clinic", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.VetClinics", "Address", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VetClinics", "Address", c => c.String());
            AlterColumn("dbo.VetClinics", "Clinic", c => c.String());
        }
    }
}
