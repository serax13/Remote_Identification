using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Identity_App.Data
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options):base(options)
        {

        }

        public DbSet<Client> SavedClients { get; set; }
        public DbSet<Img> SavedImg { get; set; }
    }
}
