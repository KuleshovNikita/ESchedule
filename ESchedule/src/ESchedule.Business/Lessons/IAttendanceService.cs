namespace ESchedule.Business.Lessons
{
    public interface IAttendanceService
    {
        Task TickPupilAttendance(Guid pupilId);
    }
}
