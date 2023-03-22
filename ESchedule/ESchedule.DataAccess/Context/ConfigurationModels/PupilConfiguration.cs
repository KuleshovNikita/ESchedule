using ESchedule.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchedule.DataAccess.Context.ConfigurationModels
{
    public record PupilConfiguration : IEntityTypeConfiguration<PupilModel>
    {
        public void Configure(EntityTypeBuilder<PupilModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.LastName)
                .IsRequired();

            builder.Property(x => x.FatherName);

            builder.Property(x => x.Age)
                .IsRequired();

            builder.Property(x => x.Password)
                .IsRequired();

            builder.HasIndex(x => x.Login)
                .IsUnique();
            builder.Property(x => x.Login)
                .IsRequired();
        }
    }
}
