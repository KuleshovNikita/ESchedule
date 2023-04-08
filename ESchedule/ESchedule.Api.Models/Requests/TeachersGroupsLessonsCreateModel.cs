namespace ESchedule.Api.Models.Requests
{
    public record TeachersGroupsLessonsCreateModel
    {
        public Guid StudyGroupId { get; set; }

        public Guid TeacherId { get; set; }

        public Guid LessonId { get; set; }
    }
}
