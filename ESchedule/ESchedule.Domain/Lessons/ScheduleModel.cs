using ESchedule.Domain.Users;

namespace ESchedule.Domain.Lessons
{
    public record ScheduleModel : BaseModel
    {
        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public Guid StudyGroupId { get; set; }
        public GroupModel StudyGroup { get; set; } = null!;

        public Guid TeacherId { get; set; }
        public TeacherModel Teacher { get; set; } = null!;

        public Guid LessonId { get; set; }
        public LessonModel Lesson { get; set; } = null!;
    }
}
