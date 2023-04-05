using ESchedule.Domain.Tenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchedule.DataAccess.Context.ConfigurationModels
{
    public record TenantSettingsConfiguration : IEntityTypeConfiguration<TenantSettingsModel>
    {
        public void Configure(EntityTypeBuilder<TenantSettingsModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LessonDurationTime).IsRequired();
            builder.Property(x => x.StudyDayStartTime).IsRequired();
            builder.Property(x => x.BreaksDurationTime).IsRequired();

            builder.HasOne(x => x.Tenant)
                .WithOne(x => x.Settings)
                .HasForeignKey<TenantSettingsModel>(x => x.TenantId);
        }
    }
}
