namespace MorePractice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class helpme : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctors", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Doctors", "Surname", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Doctors", "Fathername", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Doctors", "Fathername", c => c.String());
            AlterColumn("dbo.Doctors", "Surname", c => c.String());
            AlterColumn("dbo.Doctors", "Name", c => c.String());
        }
    }
}
