using Ereceipt.Constants;
using Ereceipt.Domain.Models;
using Ereceipt.Infrastructure.Data.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Ereceipt.Infrastructure.Data.EntityFramework.Context
{
    public class EreceiptContext : DbContext
    {
        public DbSet<App> Apps { get; set; }
        public DbSet<LoyaltyCard> LoyaltyCards { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }



        public SaveChangesResult SaveChangesResult { get; private set; }

        public EreceiptContext(DbContextOptions<EreceiptContext> options) : base(options)
        {
            Database.EnsureCreated();
            SaveChangesResult = new SaveChangesResult();
            InitBaseDataIfDatabaseEmpty();
        }

        private void InitBaseDataIfDatabaseEmpty()
        {
            if (!Roles.Any())
            {
                Roles.AddRange(RolesFactory.GetAllRoles());
            }

            if (!Apps.Any())
            {
                Apps.AddRange(AppsFactory.GetApps());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SessionConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
            modelBuilder.ApplyConfiguration(new AppConfiguration());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var res = base.SaveChangesAsync(cancellationToken);
                SaveChangesResult = new SaveChangesResult
                {
                    Exception = null
                };
                return res;
            }
            catch (Exception ex)
            {
                SaveChangesResult = new SaveChangesResult
                {
                    Exception = ex
                };
                return Task.FromResult(-1);
            }
        }
    }
}
