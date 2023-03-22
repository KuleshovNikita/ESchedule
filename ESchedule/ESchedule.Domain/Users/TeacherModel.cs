using ESchedule.Domain.Lessons;
using ESchedule.Domain.Management;
using ESchedule.Domain.ManyToManyModels;

namespace ESchedule.Domain.Users
{
    public record TeacherModel : BaseUserModel
    {
        public Guid OwnGroupId { get; set; }
        public GroupModel OwnGroup { get; set; } = null!;

        public IList<ScheduleModel> StudySchedules { get; set; } = null!;
        public IList<TeachersGroupsModel> StudyGroups { get; set; } = null!;
        public IList<TeachersLessonsModel> TaughtLessons { get; set; } = null!;
    }
}
