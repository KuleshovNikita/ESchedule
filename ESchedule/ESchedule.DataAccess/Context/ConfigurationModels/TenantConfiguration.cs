using ESchedule.Domain.Tenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchedule.DataAccess.Context.ConfigurationModels
{
    public class TenantConfiguration : IEntityTypeConfiguration<TenantModel>
    {
        public void Configure(EntityTypeBuilder<TenantModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TenantName).IsRequired();
            builder.HasIndex(x => x.TenantName).IsUnique();

            builder.HasOne(x => x.Creator)
                  .WithMany()
                  .HasForeignKey(x => x.CreatorId);
        }
    }
}
