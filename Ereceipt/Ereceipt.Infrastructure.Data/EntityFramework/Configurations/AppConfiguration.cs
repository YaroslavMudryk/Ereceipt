using Ereceipt.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Ereceipt.Infrastructure.Data.EntityFramework.Configurations
{
    public class AppConfiguration : IEntityTypeConfiguration<App>
    {
        public void Configure(EntityTypeBuilder<App> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CanUseWhileDevelopment).HasConversion(
                v => v.ToJson(),
                v => v.FromJson<int[]>());

        }
    }
}