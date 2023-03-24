using ESchedule.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchedule.DataAccess.Context.ConfigurationModels
{
    public class UserCredentialsConfiguration : IEntityTypeConfiguration<UserCredentialsModel>
    {
        public void Configure(EntityTypeBuilder<UserCredentialsModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Login)
                .IsRequired();
            builder.HasIndex(x => x.Login)
                .IsUnique();

            builder.Property(x => x.Password)
                .IsRequired();

            builder.Property(x => x.IsEmailConfirmed)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}
