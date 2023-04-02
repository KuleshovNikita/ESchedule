using ESchedule.Domain.Lessons.Schedule;
using ESchedule.ServiceResulting;
using System.Linq.Expressions;

namespace ESchedule.Business.ScheduleBuilding
{
    public interface IScheduleService
    {
        Task<ServiceResult<IEnumerable<ScheduleModel>>> BuildSchedule(Guid tenantId);

        Task<ServiceResult<Empty>> RemoveWhere(Expression<Func<ScheduleModel, bool>> predicate);
    }
}
