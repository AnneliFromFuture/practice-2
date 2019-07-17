using MorePractice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MorePractice
{
    public class ClientContext : ClientPetsDbContext
    {
        public DbSet<Client> _clients { get; set; }
        public DbSet<Pet> _pets { get; set; }
    }

    public class UsersListViewModel
    {
        public IEnumerable<Client> _clients { get; set; }
        public string Name { get; set; }
    }

}