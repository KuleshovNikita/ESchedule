using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Tenant;

namespace ESchedule.Domain.Lessons
{
    public record LessonModel : BaseModel
    {   
        public string Title { get; set; } = null!;

        public Guid TenantId { get; set; }
        public TenantModel Tenant { get; set; } = null!;

        public IList<TeachersLessonsModel> ResponsibleTeachers { get; set; } = null!;
        public IList<GroupsLessonsModel> StudingGroups { get; set; } = null!;
        public IList<ScheduleModel> RelatedSchedules { get; set; } = null!;
    }
}
