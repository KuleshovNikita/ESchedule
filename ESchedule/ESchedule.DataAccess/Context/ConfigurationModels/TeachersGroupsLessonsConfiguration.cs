using ESchedule.Domain.ManyToManyModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchedule.DataAccess.Context.ConfigurationModels
{
    public record TeachersGroupsLessonsConfiguration : IEntityTypeConfiguration<TeachersGroupsLessonsModel>
    {
        public void Configure(EntityTypeBuilder<TeachersGroupsLessonsModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Teacher)
                .WithMany(x => x.StudyGroups)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.StudyGroup)
                .WithMany(x => x.GroupTeachersLessons)
                .HasForeignKey(x => x.StudyGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Lesson)
                .WithMany()
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
