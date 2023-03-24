using ESchedule.DataAccess.Context.ConfigurationModels;
using Microsoft.EntityFrameworkCore;

namespace ESchedule.DataAccess.Context
{
    public class EScheduleDbContext : DbContext
    {
        public EScheduleDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PupilConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new SettingsConfiguration());
            modelBuilder.ApplyConfiguration(new TeachersGroupsConfiguration());
            modelBuilder.ApplyConfiguration(new TeachersLessonsConfiguration());
            modelBuilder.ApplyConfiguration(new GroupsLessonsConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new UserCredentialsConfiguration());
        }
    }
}
