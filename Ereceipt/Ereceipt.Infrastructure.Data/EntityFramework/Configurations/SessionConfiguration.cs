using Ereceipt.Domain.Models;
using Extensions.DeviceDetector.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Ereceipt.Infrastructure.Data.EntityFramework.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Location).HasConversion(
                v => v.ToJson(),
                v => v.FromJson<Location>());

            builder.Property(x => x.Device).HasConversion(
                v => v.ToJson(),
                v => v.FromJson<ClientInfo>());

            builder.Property(x => x.UnActiveFromDevice).HasConversion(
                v => v.ToJson(),
                v => v.FromJson<ClientInfo>());

            builder.Property(x => x.App).HasConversion(
                v => v.ToJson(),
                v => v.FromJson<App>());
        }
    }
}