using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchedule.DataAccess.Context.ConfigurationModels
{
    public class RequestTenantAccessConfiguration : IEntityTypeConfiguration<RequestTenantAccessModel>
    {
        public void Configure(EntityTypeBuilder<RequestTenantAccessModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TenantId);
            builder.HasOne<TenantModel>()
                    .WithMany()
                    .HasForeignKey("TenantId")
                    .OnDelete(DeleteBehavior.Cascade);


            builder.Property(x => x.UserId);
            builder.HasOne<UserModel>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
