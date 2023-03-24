using ESchedule.Domain.Enums;
using ESchedule.Domain.Lessons;
using ESchedule.Domain.ManyToManyModels;
using System.ComponentModel.DataAnnotations;

namespace ESchedule.Domain.Users
{
    public abstract record UserModel : BaseModel
    {
        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string FatherName { get; set; } = null!;

        [Range(5, 99)]
        public int Age { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsEmailConfirmed { get; set; }

        public Role Role { get; set; }

        public Guid GroupId { get; set; }
        public GroupModel Group { get; set; } = null!;

        public IList<ScheduleModel> StudySchedules { get; set; } = null!;
        public IList<TeachersGroupsModel> StudyGroups { get; set; } = null!;
        public IList<TeachersLessonsModel> TaughtLessons { get; set; } = null!;
    }
}
