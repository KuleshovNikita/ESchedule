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
            builder.Property(x => x.TenantId);

            builder.HasOne(x => x.StudyGroup)
                .WithMany(x => x.StudingLessons)
                .HasForeignKey(x => x.StudyGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Lesson)
                .WithMany(x => x.StudingGroups)
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
