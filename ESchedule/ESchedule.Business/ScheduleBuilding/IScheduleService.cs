using ESchedule.Api.Models.Requests;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.ServiceResulting;
using System.Linq.Expressions;

namespace ESchedule.Business.ScheduleBuilding
{
    public interface IScheduleService : IBaseService<ScheduleModel>
    {
        Task<ServiceResult<Empty>> BuildSchedule(Guid tenantId, IEnumerable<RuleInputModel> rules);

        Task<ServiceResult<Empty>> RemoveWhere(Expression<Func<ScheduleModel, bool>> predicate);
    }
}
