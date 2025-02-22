using ESchedule.Api.Models.Requests.Create.Schedules.Rules;
using ESchedule.Api.Models.Responses;
using ESchedule.Business.ScheduleRules;
using ESchedule.Business.Tenant;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.Enums;
using ESchedule.Domain.Lessons;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.Schedule;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using Microsoft.Extensions.Logging;
using PowerInfrastructure.AutoMapper;
using System.Data;
using System.Linq.Expressions;

namespace ESchedule.Business.ScheduleBuilding;

public class ScheduleService(
    IBaseService<GroupModel> groupService, 
    IBaseService<UserModel> teacherService,
    IBaseService<LessonModel> lessonService, 
    ITenantService tenantService,
    IScheduleBuilder scheduleBuilder, 
    IRepository<ScheduleModel> repo, 
    IMainMapper mapper,
    IBaseService<RuleModel> ruleService, 
    ITenantContextProvider tenantContextProvider,
    ILogger<ScheduleService> logger
)
    : BaseService<ScheduleModel>(repo, mapper), IScheduleService, IBaseService<ScheduleModel>
{
    public async Task BuildSchedule()
    {
        logger.LogInformation("Building schedule on tenant {tenantId}", tenantContextProvider.Current.TenantId);

        var rules = await ruleService.GetItems();

        var builderData = await GetNecessaryBuilderData();
        var parsedRules = new RulesParser().ParseToRules(rules);
        var schedules = scheduleBuilder.BuildSchedules(builderData, parsedRules);

        await RemoveAll();
        await InsertMany(schedules);
    }

    private async Task InsertRules(IEnumerable<RuleCreateModel> rules, Guid tenantId)
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

        await ruleService.InsertMany(ruleModels);
    }

    public async Task RemoveAll()
    {
        var items = await GetItems();
        await Repository.RemoveRange(items);
    }

    public override async Task InsertMany(IEnumerable<ScheduleModel> schedulesSet)
    {
        // тут долна быть валидация, но надо проверить как валидирует модельки апи Model.IsValid, может в бинесе и не придется ничего валидировать
        await Repository.InsertMany(schedulesSet);
    }

    public async Task<IEnumerable<ScheduleItemResponse>> GetMinimalSchedule(Expression<Func<ScheduleModel, bool>> predicate)
    {
        var schedules = await base.GetItems(predicate);

        return schedules.Select(x => Mapper.Map<ScheduleItemResponse>(x));
    }

    private async Task<ScheduleBuilderHelpData> GetNecessaryBuilderData()
    {
        var groups = await groupService.Where(x => true);
        var teachers = await teacherService.GetItems(x => x.Role == Role.Teacher);
        var tenant = await tenantService.SingleOrDefault(x => x.Id == tenantContextProvider.Current.TenantId);
        var lessons = await lessonService.Where(x => true);

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