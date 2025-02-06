using ESchedule.Domain.Lessons;
using ESchedule.Domain.Users;

namespace ESchedule.Domain.ManyToManyModels;

public record GroupsLessonsModel : BaseModel
{
    public Guid LessonId { get; set; }
    public LessonModel Lesson { get; set; } = null!;

    public Guid StudyGroupId { get; set; }
    public GroupModel StudyGroup { get; set; } = null!;

    public Guid TenantId { get; set; }
}
