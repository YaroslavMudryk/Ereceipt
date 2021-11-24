using Ereceipt.Domain.Models;
using Ereceipt.Infrastructure.Data.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

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
                var newEntities = this.ChangeTracker.Entries().Where(x => x.Entity is BaseModel && x.State == EntityState.Added).Select(x => x.Entity as BaseModel);
                foreach (var entity in newEntities)
                {
                    entity.CreatedAt = DateTime.Now;
                }
                var updateEntities = this.ChangeTracker.Entries().Where(x => x.Entity is BaseModel && x.State == EntityState.Modified).Select(x => x.Entity as BaseModel);
                foreach (var entity in updateEntities)
                {
                    entity.LastUpdatedAt = DateTime.Now;
                }
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
