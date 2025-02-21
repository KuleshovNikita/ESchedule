using ESchedule.Domain.Users;

namespace ESchedule.Api.Models.Responses;

public class ScheduleItemResponse
{
    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public string GroupName { get; set; } = null!;

    public UserModel Teacher { get; set; } = null!;

    public string LessonName { get; set; } = null!;
}
