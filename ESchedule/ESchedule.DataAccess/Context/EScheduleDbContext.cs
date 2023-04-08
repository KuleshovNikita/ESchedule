using ESchedule.Core.Interfaces;
using ESchedule.DataAccess.Context.ConfigurationModels;
using ESchedule.Domain.Lessons;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ESchedule.DataAccess.Context
{
    public class EScheduleDbContext : DbContext
    {
        private readonly IPasswordHasher _passwordHasher;

        public DbSet<UserModel> Users { get; set; }
        public DbSet<LessonModel> Lessons { get; set; }
        public DbSet<ScheduleModel> Schedules { get; set; }
        public DbSet<RuleModel> ScheduleRules { get; set; }
        public DbSet<TenantSettingsModel> TenantSettings { get; set; }
        public DbSet<TenantModel> Tenant { get; set; }
        public DbSet<GroupModel> Groups { get; set; }
        public DbSet<GroupsLessonsModel> GroupsLessons { get; set; }
        public DbSet<TeachersGroupsLessonsModel> TeachersGroupsLessons { get; set; }
        public DbSet<TeachersLessonsModel> TeachersLessons { get; set; }

        public EScheduleDbContext(DbContextOptions options, IPasswordHasher passwordHasher) : base(options)
        {
            _passwordHasher = passwordHasher;
        }

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

            Seeds.ApplySeeds(modelBuilder, _passwordHasher);
        }
    }
}
