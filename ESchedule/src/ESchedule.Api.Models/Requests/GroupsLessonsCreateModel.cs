namespace ESchedule.Api.Models.Requests
{
    public record GroupsLessonsCreateModel
    {
        public Guid LessonId { get; set; }

        public Guid StudyGroupId { get; set; }
    }
}
