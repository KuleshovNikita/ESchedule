using ESchedule.Domain.ManyToManyModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchedule.DataAccess.Context.ConfigurationModels
{
    public record GroupsLessonsConfiguration : IEntityTypeConfiguration<GroupsLessonsModel>
    {
        public void Configure(EntityTypeBuilder<GroupsLessonsModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.StudyGroup)
                .WithMany(x => x.StudingLessons)
                .HasForeignKey(x => x.StudyGroupId);

            builder.HasOne(x => x.Lesson)
                .WithMany(x => x.StudingGroups)
                .HasForeignKey(x => x.LessonId);
        }
    }
}
