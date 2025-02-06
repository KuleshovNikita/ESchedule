using ESchedule.Domain.Schedule.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchedule.DataAccess.Context.ConfigurationModels;

public class ScheduleRulesConfiguration : IEntityTypeConfiguration<RuleModel>
{
    public void Configure(EntityTypeBuilder<RuleModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
