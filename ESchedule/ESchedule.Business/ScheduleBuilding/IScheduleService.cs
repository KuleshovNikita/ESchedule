using ESchedule.Api.Models.Requests;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.ServiceResulting;
using System.Linq.Expressions;

namespace ESchedule.Business.ScheduleBuilding
{
    public interface IScheduleService : IBaseService<ScheduleModel>
    {
        Task BuildSchedule(Guid tenantId);

        Task RemoveWhere(Expression<Func<ScheduleModel, bool>> predicate);
    }
}
