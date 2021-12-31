using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Identification.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Identification.Api
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {

        }

        public DbSet<Client> SavedClients { get; set; }
        public DbSet<Img> SavedImg { get; set; }
        public DbSet<User> Logins { get; set; }
        public DbSet<RoleUsers> RolesUsers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
