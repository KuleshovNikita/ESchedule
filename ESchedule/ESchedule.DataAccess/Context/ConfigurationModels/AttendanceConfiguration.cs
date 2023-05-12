using ESchedule.Domain.Lessons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchedule.DataAccess.Context.ConfigurationModels
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<AttendanceModel>
    {
        public void Configure(EntityTypeBuilder<AttendanceModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Schedule)
                .WithMany()
                .HasForeignKey(x => x.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Tenant)
                .WithMany()
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Pupil)
                .WithMany()
                .HasForeignKey(x => x.PupilId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Date)
                .IsRequired();
        }
    }
}
