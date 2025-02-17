using ESchedule.DataAccess.Context.ConfigurationModels;
using ESchedule.Domain.Lessons;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ESchedule.DataAccess.Context;

public class EScheduleDbContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<UserModel>? Users { get; set; }
    public virtual DbSet<LessonModel>? Lessons { get; set; }
    public virtual DbSet<ScheduleModel>? Schedules { get; set; }
    public virtual DbSet<RuleModel>? ScheduleRules { get; set; }
    public virtual DbSet<TenantSettingsModel>? TenantSettings { get; set; }
    public virtual DbSet<TenantModel>? Tenant { get; set; }
    public virtual DbSet<GroupModel>? Groups { get; set; }
    public virtual DbSet<GroupsLessonsModel>? GroupsLessons { get; set; }
    public virtual DbSet<TeachersGroupsLessonsModel>? TeachersGroupsLessons { get; set; }
    public virtual DbSet<TeachersLessonsModel>? TeachersLessons { get; set; }
    public virtual DbSet<AttendanceModel>? Attendances { get; set; }
    public virtual DbSet<RequestTenantAccessModel>? TenantAccessRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        modelBuilder.ApplyConfiguration(new LessonConfiguration());
        modelBuilder.ApplyConfiguration(new TenantSettingsConfiguration());
        modelBuilder.ApplyConfiguration(new TeachersGroupsLessonsConfiguration());
        modelBuilder.ApplyConfiguration(new TeachersLessonsConfiguration());
        modelBuilder.ApplyConfiguration(new GroupsLessonsConfiguration());
        modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new TenantConfiguration());
        modelBuilder.ApplyConfiguration(new ScheduleRulesConfiguration());
        modelBuilder.ApplyConfiguration(new AttendanceConfiguration());
        modelBuilder.ApplyConfiguration(new RequestTenantAccessConfiguration());

        Seeds.ApplySeeds(modelBuilder);
    }
}
