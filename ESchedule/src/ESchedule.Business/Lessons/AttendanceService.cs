using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Lessons;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ESchedule.Business.Lessons;

public class AttendanceService(EScheduleDbContext context) : IAttendanceService
{
    public async Task TickPupilAttendance(Guid pupilId)
    {
        var user = await context.Users
                            .FirstOrDefaultAsync(x => x.Id == pupilId);

        if (user == null)
        {
            throw new EntityNotFoundException(Resources.NoUsersForSpecifiedKeyWereFound);
        }

        var currentTime = DateTime.Now;
        ScheduleModel schedule = null!;

        if (schedule == null)
        {
            schedule = new ScheduleModel
            {
                Id = Guid.NewGuid(),
                StartTime = currentTime.TimeOfDay,
                EndTime = currentTime.TimeOfDay,
                DayOfWeek = DateTime.Today.DayOfWeek,
                StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                LessonId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            };

            context.Schedules.Add(schedule);
            context.SaveChanges();
        }

        var model = new AttendanceModel
        {
            Id = Guid.NewGuid(),
            ScheduleId = schedule.Id,
            PupilId = pupilId,
            TenantId = schedule.TenantId,
            Date = currentTime,
        };

        await context.Attendances.AddAsync(model);
        await context.SaveChangesAsync();
    }

    private ScheduleModel? GetTargetSchedule(UserModel user, TimeSpan currentTime)
    {
        var groupId = user.GroupId;

        return context.Schedules
            .AsEnumerable()
            .FirstOrDefault(x => x.StartTime <= currentTime && x.EndTime >= currentTime && groupId == x.StudyGroupId && x.DayOfWeek == DateTime.Today.DayOfWeek);
    }
}