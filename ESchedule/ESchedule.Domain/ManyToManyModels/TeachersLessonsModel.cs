using ESchedule.Domain.Lessons;
using ESchedule.Domain.Users;

namespace ESchedule.Domain.ManyToManyModels
{
    public record TeachersLessonsModel : BaseModel
    {
        public Guid LessonId { get; set; }
        public LessonModel Lesson { get; set; } = null!;

        public Guid TeacherId { get; set; }
        public UserModel Teacher { get; set; } = null!;
    }
}
