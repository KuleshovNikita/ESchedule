using ESchedule.Domain.Lessons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchedule.DataAccess.Context.ConfigurationModels
{
    public record ScheduleConfiguration : IEntityTypeConfiguration<ScheduleModel>
    {
        public void Configure(EntityTypeBuilder<ScheduleModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.StartTime)
                .IsRequired();

            builder.Property(x => x.EndTime)
                .IsRequired();

            builder.Property(x => x.DayOfWeek)
                .IsRequired();

            builder.HasOne(x => x.StudyGroup)
                .WithMany(x => x.StudySchedules)
                .HasForeignKey(x => x.StudyGroupId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Teacher)
                .WithMany(x => x.StudySchedules)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Lesson)
                .WithMany(x => x.RelatedSchedules)
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Tenant)
                .WithMany()
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
