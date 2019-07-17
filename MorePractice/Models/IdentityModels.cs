using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace MorePractice.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class Client : IdentityUser
    {
        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public string Fathername { get; set; }

        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pet> Pets { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Client> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ClientPetsDbContext : IdentityDbContext<Client>
    {
        public ClientPetsDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ClientPetsDbContext Create()
        {
            return new ClientPetsDbContext();
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Kind> Kind { get; set; }
        public DbSet<Reception> Reception { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<ServiceDoctor> ServiceDoctor { get; set; }
        public DbSet<Specializ> Specializ { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<VetClinic> VetClinic { get; set; }
    }
}