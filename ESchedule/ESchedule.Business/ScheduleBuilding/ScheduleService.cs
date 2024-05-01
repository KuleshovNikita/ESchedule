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

        public async Task BuildSchedule()
        {
            var rules = await _rulesService.GetItems();

            var builderData = await GetNecessaryBuilderData();
            var parsedRules = new RulesParser().ParseToRules(rules);
            var schedules = _scheduleBuilder.BuildSchedules(builderData, parsedRules);

            await RemoveAll();
            await InsertMany(schedules);
        }

        private async Task InsertRules(IEnumerable<RuleInputModel> rules, Guid tenantId)
        {
            var ruleModels = rules.Select(r => 
                new RuleModel
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    RuleName = r.RuleName,
                    RuleJson = r.RuleJson,
                }
            );

            await _rulesService.InsertMany(ruleModels);
        }

        public async Task RemoveAll()
        {
            var items = await GetItems();
            await _repository.RemoveRange(items);
        }

        public override async Task InsertMany(IEnumerable<ScheduleModel> schedulesSet)
        {
            // тут долна быть валидация, но надо проверить как валидирует модельки апи Model.IsValid, может в бинесе и не придется ничего валидировать
            await _repository.InsertMany(schedulesSet);
        }

        public override async Task<IEnumerable<ScheduleModel>> GetItems(Expression<Func<ScheduleModel, bool>> predicate, bool includeNavs = false)
        {
            var schedules = await base.GetItems(predicate);
            var result = includeNavs ? schedules : SimplifySchedulesSet(schedules);

            return result;
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

        private async Task<ScheduleBuilderHelpData> GetNecessaryBuilderData()
        {
            var groups = await _groupService.GetItems();
            var teachers = await _teacherService.GetItems(x => x.Role == Role.Teacher);
            var tenant = await _tenantService.SingleOrDefault();
            var lessons = await _lessonService.GetItems();

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
