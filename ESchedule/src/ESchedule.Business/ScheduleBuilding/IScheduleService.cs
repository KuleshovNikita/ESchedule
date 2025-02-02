using ESchedule.Api.Models.Responses;
using ESchedule.Domain.Lessons.Schedule;
using System.Linq.Expressions;

namespace ESchedule.Business.ScheduleBuilding
{
    public interface IScheduleService : IBaseService<ScheduleModel>
    {
        Task BuildSchedule();

        Task RemoveAll();

        Task<IEnumerable<ScheduleItemResponse>> GetMinimalSchedule(Expression<Func<ScheduleModel, bool>> predicate);
    }
}
