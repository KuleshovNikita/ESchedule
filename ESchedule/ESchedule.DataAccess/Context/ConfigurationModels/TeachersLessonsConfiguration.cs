using ESchedule.Domain.ManyToManyModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchedule.DataAccess.Context.ConfigurationModels
{
    public record TeachersLessonsConfiguration : IEntityTypeConfiguration<TeachersLessonsModel>
    {
        public void Configure(EntityTypeBuilder<TeachersLessonsModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Teacher)
                .WithMany(x => x.TaughtLessons)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Lesson)
                .WithMany(x => x.ResponsibleTeachers)
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
