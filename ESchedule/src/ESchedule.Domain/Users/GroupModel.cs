using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Tenant;

namespace ESchedule.Domain.Users;

public record GroupModel : BaseModel
{
    public string Title { get; set; } = null!;

    public int MaxLessonsCountPerDay { get; set; }

    public Guid TenantId { get; set; }
    public TenantModel Tenant { get; set; } = null!;

    public IList<UserModel> Members { get; set; } = new List<UserModel>();
    public IList<TeachersGroupsLessonsModel> GroupTeachersLessons { get; set; } = new List<TeachersGroupsLessonsModel>();
    public IList<ScheduleModel> StudySchedules { get; set; } = new List<ScheduleModel>();
    public IList<GroupsLessonsModel> StudingLessons { get; set; } = new List<GroupsLessonsModel>();
}
