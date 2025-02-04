using ESchedule.Domain.Lessons;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;

namespace ESchedule.Domain.Schedule
{
    public class ScheduleBuilderHelpData
    {
        public IReadOnlyCollection<GroupModel> AllTenantGroups { get; set; } = null!;

        public IReadOnlyCollection<LessonModel> AllTenantLessons { get; set; } = null!;

        public IReadOnlyCollection<UserModel> AllTenantTeachers { get; set; } = null!;

        public TenantModel TargetTenant { get; set; } = null!;
    }
}
