using ESchedule.Domain.Users;

namespace ESchedule.Domain.ManyToManyModels
{
    public record TeachersGroupsModel : BaseModel
    {
        public Guid StudyGroupId { get; set; }
        public GroupModel StudyGroup { get; set; } = null!;

        public Guid TeacherId { get; set; }
        public TeacherModel Teacher { get; set; } = null!;
    }
}
