using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;

namespace ESchedule.Domain.Lessons;

public record AttendanceModel : BaseModel
{
    public Guid PupilId { get; set; }
    public UserModel Pupil { get; set; } = null!;

    public Guid ScheduleId { get; set; }
    public ScheduleModel Schedule { get; set; } = null!;

    public Guid TenantId { get; set; }
    public TenantModel Tenant { get; set; } = null!;

    public DateTime Date { get; set; }
}
