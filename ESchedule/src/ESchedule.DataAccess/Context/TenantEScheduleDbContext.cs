using ESchedule.Domain.Lessons;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ESchedule.DataAccess.Context;

public class TenantEScheduleDbContext(
    DbContextOptions options,
    ITenantContextProvider tenantContextProvider
)
    : EScheduleDbContext(options)
{
    public TenantContext TenantContext => tenantContextProvider.Current;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserModel>().HasQueryFilter(x => x.TenantId == TenantContext.TenantId);
        modelBuilder.Entity<TenantSettingsModel>().HasQueryFilter(x => x.TenantId == TenantContext.TenantId);
        modelBuilder.Entity<TenantModel>().HasQueryFilter(x => x.Id == TenantContext.TenantId);
        modelBuilder.Entity<ScheduleModel>().HasQueryFilter(x => x.TenantId == TenantContext.TenantId);
        modelBuilder.Entity<RuleModel>().HasQueryFilter(x => x.TenantId == TenantContext.TenantId);
        modelBuilder.Entity<LessonModel>().HasQueryFilter(x => x.TenantId == TenantContext.TenantId);
        modelBuilder.Entity<GroupModel>().HasQueryFilter(x => x.TenantId == TenantContext.TenantId);
        modelBuilder.Entity<AttendanceModel>().HasQueryFilter(x => x.TenantId == TenantContext.TenantId);
        modelBuilder.Entity<GroupsLessonsModel>().HasQueryFilter(x => x.TenantId == TenantContext.TenantId);
        modelBuilder.Entity<TeachersLessonsModel>().HasQueryFilter(x => x.TenantId == TenantContext.TenantId);
        modelBuilder.Entity<TeachersGroupsLessonsModel>().HasQueryFilter(x => x.TenantId == TenantContext.TenantId);
        modelBuilder.Entity<RequestTenantAccessModel>().HasQueryFilter(x => x.TenantId == TenantContext.TenantId);
    }
}
