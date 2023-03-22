using ESchedule.Domain.Lessons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchedule.DataAccess.Context.ConfigurationModels
{
    public record LessonConfiguration : IEntityTypeConfiguration<LessonModel>
    {
        public void Configure(EntityTypeBuilder<LessonModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(30);
            builder.HasIndex(x => x.Title)
                .IsUnique();
        }
    }
}
