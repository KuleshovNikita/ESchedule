using ESchedule.Domain.Enums;
using ESchedule.Domain.Lessons;
using ESchedule.Domain.Schedule;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;

namespace ESchedule.Business.ScheduleBuilding
{
    public class ScheduleService : IScheduleService
    {
        private readonly IBaseService<GroupModel> _groupService;
        private readonly IBaseService<UserModel> _teacherService;
        private readonly IBaseService<LessonModel> _lessonService;
        private readonly IBaseService<TenantModel> _tenantService;
        private readonly IScheduleBuilder _scheduleBuilder;

        public ScheduleService(IBaseService<GroupModel> groupService, IBaseService<UserModel> teacherService,
            IBaseService<LessonModel> lessonService, IBaseService<TenantModel> tenantService,
            IScheduleBuilder scheduleBuilder)
        {
            _groupService = groupService;
            _teacherService = teacherService;
            _lessonService = lessonService;
            _tenantService = tenantService;
            _scheduleBuilder = scheduleBuilder;
        }

        public async Task BuildSchedule(Guid tenantId)
        {
            var builderData = await GetNecessaryBuilderData(tenantId);
            _scheduleBuilder.BuildSchedule(builderData);
        }

        private async Task<ScheduleBuilderHelpData> GetNecessaryBuilderData(Guid tenantId)
        {
            var groups = (await _groupService.GetItems(x => x.TenantId == tenantId)).Value;
            var teachers = (await _teacherService.GetItems(x => x.Role == Role.Teacher && x.TenantId == tenantId)).Value;
            var tenant = (await _tenantService.First(x => x.Id == tenantId)).Value;
            var lessons = (await _lessonService.GetItems(x => x.TenantId == tenantId)).Value;

            var scheduleBuilderData = new ScheduleBuilderHelpData
            {
                AllTenantGroups = (IReadOnlyCollection<GroupModel>)groups,
                AllTenantTeachers = (IReadOnlyCollection<UserModel>)teachers,
                AllTenantLessons = (IReadOnlyCollection<LessonModel>)lessons,
                TargetTenant = tenant
            };

            return scheduleBuilderData;
        }
    }
}
