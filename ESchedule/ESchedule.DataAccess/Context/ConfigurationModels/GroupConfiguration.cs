using ESchedule.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchedule.DataAccess.Context.ConfigurationModels
{
    public record GroupConfiguration : IEntityTypeConfiguration<GroupModel>
    {
        public void Configure(EntityTypeBuilder<GroupModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x => x.Title)
                .HasMaxLength(5);
            builder.HasIndex(x => x.Title)
                .IsUnique();

            builder.Property(x => x.MaxLessonsCountPerDay)
                .IsRequired();
        }
    }
}
