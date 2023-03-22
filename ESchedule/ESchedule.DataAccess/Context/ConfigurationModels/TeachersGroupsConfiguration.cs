using ESchedule.Domain.ManyToManyModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchedule.DataAccess.Context.ConfigurationModels
{
    public record TeachersGroupsConfiguration : IEntityTypeConfiguration<TeachersGroupsModel>
    {
        public void Configure(EntityTypeBuilder<TeachersGroupsModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Teacher)
                .WithMany(x => x.StudyGroups)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.StudyGroup)
                .WithMany(x => x.GroupTeachers)
                .HasForeignKey(x => x.StudyGroupId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
