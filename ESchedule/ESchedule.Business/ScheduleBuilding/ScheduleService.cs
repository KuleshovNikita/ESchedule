using AutoMapper;
using ESchedule.Api.Models.Requests;
using ESchedule.Business.ScheduleRules;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.Enums;
using ESchedule.Domain.Lessons;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.Schedule;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using System.Data;
using System.Linq.Expressions;

namespace ESchedule.Business.ScheduleBuilding
{
    public class ScheduleService : BaseService<ScheduleModel>, IScheduleService, IBaseService<ScheduleModel>
    {
        private readonly IBaseService<GroupModel> _groupService;
        private readonly IBaseService<RuleModel> _rulesService;
        private readonly IBaseService<UserModel> _teacherService;
        private readonly IBaseService<LessonModel> _lessonService;
        private readonly IBaseService<TenantModel> _tenantService;
        private readonly IScheduleBuilder _scheduleBuilder;

        public ScheduleService(IBaseService<GroupModel> groupService, IBaseService<UserModel> teacherService,
            IBaseService<LessonModel> lessonService, IBaseService<TenantModel> tenantService,
            IScheduleBuilder scheduleBuilder, IRepository<ScheduleModel> repo, IMapper mapper,
            IBaseService<RuleModel> ruleService) 
            : base(repo, mapper)
        {
            _groupService = groupService;
            _teacherService = teacherService;
            _lessonService = lessonService;
            _tenantService = tenantService;
            _scheduleBuilder = scheduleBuilder;
            _rulesService = ruleService;
        }

        public async Task<ServiceResult<Empty>> BuildSchedule(Guid tenantId, IEnumerable<RuleInputModel> jsonRules)
        {
            if(jsonRules == null)
            {
                throw new ArgumentNullException(nameof(jsonRules));
            }

            var builderData = await GetNecessaryBuilderData(tenantId);
            var parsedRules = new RulesParser().ParseToRules(jsonRules);
            var schedules = _scheduleBuilder.BuildSchedules(builderData, parsedRules);

            (await InsertMany(schedules)).Success();

            return await InsertRules(jsonRules, tenantId);
        }

        private async Task<ServiceResult<Empty>> InsertRules(IEnumerable<RuleInputModel> rules, Guid tenantId)
        {
            var ruleModels = rules.Select(r => 
                new RuleModel
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    RuleName = r.RuleName,
                    RuleJson = r.JsonBody,
                }
            );

            return (await _rulesService.InsertMany(ruleModels)).Success();
        }

        public async Task<ServiceResult<Empty>> RemoveWhere(Expression<Func<ScheduleModel, bool>> predicate)
        {
            var items = await GetItems(predicate);
            return (await _repository.RemoveRange(items.Value)).Success();
        }

        public override async Task<ServiceResult<Empty>> InsertMany(IEnumerable<ScheduleModel> schedulesSet)
        {
            // тут долна быть валидация, но надо проверить как валидирует модельки апи Model.IsValid, может в бинесе и не придется ничего валидировать
            return (await _repository.InsertMany(schedulesSet)).Success();
        }

        public override async Task<ServiceResult<IEnumerable<ScheduleModel>>> GetItems(Expression<Func<ScheduleModel, bool>> predicate)
        {
            var schedules = (await base.GetItems(predicate)).Value;

            return new ServiceResult<IEnumerable<ScheduleModel>>
            {
                Value = SimplifySchedulesSet(schedules)
            }.Success();
        }

        private IEnumerable<ScheduleModel> SimplifySchedulesSet(IEnumerable<ScheduleModel> schedulesSet)
        {
            foreach (var schedule in schedulesSet)
            {
                schedule.Teacher = null!;
                schedule.StudyGroup = null!;
                schedule.Lesson = null!;
                schedule.Tenant = null!;
            }

            return schedulesSet;
        }

        private async Task<ScheduleBuilderHelpData> GetNecessaryBuilderData(Guid tenantId)
        {
            var groups = (await _groupService.GetItems(x => x.TenantId == tenantId)).Value;
            var teachers = (await _teacherService.GetItems(x => x.Role == Role.Teacher && x.TenantId == tenantId)).Value;
            var tenant = (await _tenantService.First(x => x.Id == tenantId)).Value;
            var lessons = (await _lessonService.GetItems(x => x.TenantId == tenantId)).Value;

            var scheduleBuilderData = new ScheduleBuilderHelpData
            {
                AllTenantGroups = groups.ToList().AsReadOnly(),
                AllTenantTeachers = teachers.ToList().AsReadOnly(),
                AllTenantLessons = lessons.ToList().AsReadOnly(),
                TargetTenant = tenant
            };

            return scheduleBuilderData;
        }
    }
}
