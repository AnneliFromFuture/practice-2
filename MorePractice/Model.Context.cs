﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MorePractice
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Veterinary_clinicEntities : DbContext
    {
        public Veterinary_clinicEntities()
            : base("name=Veterinary_clinicEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Kind> Kind { get; set; }
        public virtual DbSet<Pet> Pet { get; set; }
        public virtual DbSet<Reception> Reception { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceDoctor> ServiceDoctor { get; set; }
        public virtual DbSet<Specializ> Specializ { get; set; }
        public virtual DbSet<Species> Species { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<VetClinic> VetClinic { get; set; }
    }
}
