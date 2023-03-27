using ESchedule.Domain.Lessons;
using ESchedule.Domain.ManyToManyModels;

namespace ESchedule.Domain.Users
{
    public record GroupModel : BaseModel
    {
        public string Title { get; set; } = null!;

        public int MaxLessonsCountPerDay { get; set; }

        public IList<UserModel> Members { get; set; } = null!;
        public IList<TeachersGroupsModel> GroupTeachers { get; set; } = null!;
        public IList<ScheduleModel> StudySchedules { get; set; } = null!;
        public IList<GroupsLessonsModel> StudingLessons { get; set; } = null!;
    }
}
