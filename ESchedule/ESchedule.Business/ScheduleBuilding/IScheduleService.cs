using ESchedule.Business.ScheduleRules;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.ServiceResulting;
using System.Linq.Expressions;

namespace ESchedule.Business.ScheduleBuilding
{
    public interface IScheduleService : IBaseService<ScheduleModel>
    {
        Task<ServiceResult<Empty>> BuildSchedule(Guid tenantId, IEnumerable<BaseScheduleRule> rules);

        Task<ServiceResult<Empty>> RemoveWhere(Expression<Func<ScheduleModel, bool>> predicate);
    }
}
