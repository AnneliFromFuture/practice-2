namespace MorePractice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pets",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        IsServiced = c.Boolean(nullable: false),
                        ID_Species = c.Guid(nullable: false),
                        OwnerId = c.String(maxLength: 128),
                        Species_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .ForeignKey("dbo.Species", t => t.Species_ID)
                .Index(t => t.OwnerId)
                .Index(t => t.Species_ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        Fathername = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Receptions",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RecTime = c.DateTime(nullable: false),
                        EmailNotification = c.Boolean(nullable: false),
                        ID_Pet = c.Guid(nullable: false),
                        ID_Service = c.Guid(nullable: false),
                        ID_Doctor = c.Guid(nullable: false),
                        Doctor_ID = c.Guid(),
                        Service_ID = c.Guid(),
                        Pet_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Doctors", t => t.Doctor_ID)
                .ForeignKey("dbo.Services", t => t.Service_ID)
                .ForeignKey("dbo.Pets", t => t.Pet_ID)
                .Index(t => t.Doctor_ID)
                .Index(t => t.Service_ID)
                .Index(t => t.Pet_ID);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Surname = c.String(),
                        Fathername = c.String(),
                        ID_Clinic = c.Guid(nullable: false),
                        ID_Special = c.Guid(nullable: false),
                        Specializ_ID = c.Guid(),
                        VetClinic_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Specializs", t => t.Specializ_ID)
                .ForeignKey("dbo.VetClinics", t => t.VetClinic_ID)
                .Index(t => t.Specializ_ID)
                .Index(t => t.VetClinic_ID);
            
            CreateTable(
                "dbo.ServiceDoctors",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ID_Service = c.Guid(nullable: false),
                        ID_Doctor = c.Guid(nullable: false),
                        Doctor_ID = c.Guid(),
                        Service_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Doctors", t => t.Doctor_ID)
                .ForeignKey("dbo.Services", t => t.Service_ID)
                .Index(t => t.Doctor_ID)
                .Index(t => t.Service_ID);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PetService = c.String(),
                        Price = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Specializs",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Specialization = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.VetClinics",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Clinic = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Species",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PetSpecies = c.String(),
                        ID_Kind = c.Guid(nullable: false),
                        Kind_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Kinds", t => t.Kind_ID)
                .Index(t => t.Kind_ID);
            
            CreateTable(
                "dbo.Kinds",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PetKind = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Pets", "Species_ID", "dbo.Species");
            DropForeignKey("dbo.Species", "Kind_ID", "dbo.Kinds");
            DropForeignKey("dbo.Receptions", "Pet_ID", "dbo.Pets");
            DropForeignKey("dbo.Doctors", "VetClinic_ID", "dbo.VetClinics");
            DropForeignKey("dbo.Doctors", "Specializ_ID", "dbo.Specializs");
            DropForeignKey("dbo.ServiceDoctors", "Service_ID", "dbo.Services");
            DropForeignKey("dbo.Receptions", "Service_ID", "dbo.Services");
            DropForeignKey("dbo.ServiceDoctors", "Doctor_ID", "dbo.Doctors");
            DropForeignKey("dbo.Receptions", "Doctor_ID", "dbo.Doctors");
            DropForeignKey("dbo.Pets", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Species", new[] { "Kind_ID" });
            DropIndex("dbo.ServiceDoctors", new[] { "Service_ID" });
            DropIndex("dbo.ServiceDoctors", new[] { "Doctor_ID" });
            DropIndex("dbo.Doctors", new[] { "VetClinic_ID" });
            DropIndex("dbo.Doctors", new[] { "Specializ_ID" });
            DropIndex("dbo.Receptions", new[] { "Pet_ID" });
            DropIndex("dbo.Receptions", new[] { "Service_ID" });
            DropIndex("dbo.Receptions", new[] { "Doctor_ID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Pets", new[] { "Species_ID" });
            DropIndex("dbo.Pets", new[] { "OwnerId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Kinds");
            DropTable("dbo.Species");
            DropTable("dbo.VetClinics");
            DropTable("dbo.Specializs");
            DropTable("dbo.Services");
            DropTable("dbo.ServiceDoctors");
            DropTable("dbo.Doctors");
            DropTable("dbo.Receptions");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Pets");
        }
    }
}
