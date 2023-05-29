using ESchedule.ServiceResulting;

namespace ESchedule.Business.Lessons
{
    public interface IAttendanceService
    {
        Task<ServiceResult<Empty>> TickPupilAttendance(Guid pupilId);
    }
}
