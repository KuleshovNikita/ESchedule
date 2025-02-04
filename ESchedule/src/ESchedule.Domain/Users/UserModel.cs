using ESchedule.Domain.Enums;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Tenant;
using System.ComponentModel.DataAnnotations;

namespace ESchedule.Domain.Users
{
    public record UserModel : BaseModel
    {
        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string FatherName { get; set; } = null!;

        [Range(5, 99)]
        public int Age { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public Role Role { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public Guid? GroupId { get; set; }
        public GroupModel? Group { get; set; } = null!;

        public Guid? TenantId { get; set; }
        public TenantModel? Tenant { get; set; } = null!;

        public IList<ScheduleModel> StudySchedules { get; set; } = new List<ScheduleModel>();
        public IList<TeachersGroupsLessonsModel> StudyGroups { get; set; } = new List<TeachersGroupsLessonsModel>();
        public IList<TeachersLessonsModel> TaughtLessons { get; set; } = new List<TeachersLessonsModel>();
    }
}
