using ESchedule.Domain.Enums;
using ESchedule.Domain.Lessons;
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

        public ScheduleService(IBaseService<GroupModel> groupService, IBaseService<UserModel> teacherService,
            IBaseService<LessonModel> lessonService, IBaseService<TenantModel> tenantService)
        {
            _groupService = groupService;
            _teacherService = teacherService;
            _lessonService = lessonService;
            _tenantService = tenantService;
        }

        public void BuildSchedule(Guid tenantId)
        {
            var builderData = GetNecessaryBuilderData(tenantId);
        }

        private async Task<object> GetNecessaryBuilderData(Guid tenantId)
        {
            var groups = await _groupService.GetItems(x => x.TenantId == tenantId);
            var teachers = await _teacherService.GetItems(x => x.Role == Role.Teacher && x.TenantId == tenantId);
            var lessons = await _lessonService.GetItems(x => x.TenantId == tenantId); //достает все уроки, нет разделения на предметы,
                                                                                      //которые читаются у отдельных классов
            var tenant = await _tenantService.First(x => x.Id == tenantId);
        }
    }
}
