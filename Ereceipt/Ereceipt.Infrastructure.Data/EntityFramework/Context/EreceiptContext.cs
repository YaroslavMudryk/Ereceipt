using Ereceipt.Domain.Models;
using Ereceipt.Infrastructure.Data.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Ereceipt.Infrastructure.Data.EntityFramework.Context
{
    public class EreceiptContext : DbContext
    {
        public DbSet<App> Apps { get; set; }
        public DbSet<LoyaltyCard> LoyaltyCards { get; set;}
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }




        public EreceiptContext(DbContextOptions<EreceiptContext> options): base(options)
        {
            Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SessionConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
