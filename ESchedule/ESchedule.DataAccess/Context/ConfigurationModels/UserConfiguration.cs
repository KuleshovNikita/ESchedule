using ESchedule.Core.Interfaces;
using ESchedule.Domain.Enums;
using ESchedule.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchedule.DataAccess.Context.ConfigurationModels
{
    public record UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
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

            builder.Property(x => x.IsEmailConfirmed)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(x => x.Role)
                .IsRequired();

            builder.HasOne(x => x.Group)
                .WithMany(x => x.Members)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Tenant)
                .WithMany()
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
