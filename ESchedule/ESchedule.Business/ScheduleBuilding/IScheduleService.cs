namespace ESchedule.Business.ScheduleBuilding
{
    public interface IScheduleService
    {
        Task BuildSchedule(Guid tenantId);
    }
}
