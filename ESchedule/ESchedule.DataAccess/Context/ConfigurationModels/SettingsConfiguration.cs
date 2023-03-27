using ESchedule.Domain.Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchedule.DataAccess.Context.ConfigurationModels
{
    public record SettingsConfiguration : IEntityTypeConfiguration<SettingsModel>
    {
        public void Configure(EntityTypeBuilder<SettingsModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LessonDurationTime).IsRequired();
            builder.Property(x => x.StudyDayStartTime).IsRequired();
            builder.Property(x => x.BreaksDurationTime).IsRequired();

            builder.HasOne(x => x.Creator)
                .WithOne()
                .HasForeignKey<SettingsModel>(x => x.CreatorId);

            builder.HasOne(x => x.Tenant)
                .WithOne()
                .HasForeignKey<SettingsModel>(x => x.TenantId);
        }
    }
}
