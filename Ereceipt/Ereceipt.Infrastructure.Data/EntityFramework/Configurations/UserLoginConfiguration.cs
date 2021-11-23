using Ereceipt.Domain.Models;
using Extensions.DeviceDetector.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Ereceipt.Infrastructure.Data.EntityFramework.Configurations
{
    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.RegisterFromDevice).HasConversion(
                v => v.ToJson(),
                v => v.FromJson<ClientInfo>());

            builder.Property(x => x.ConfirmFromDevice).HasConversion(
                v => v.ToJson(),
                v => v.FromJson<ClientInfo>());
        }
    }
}