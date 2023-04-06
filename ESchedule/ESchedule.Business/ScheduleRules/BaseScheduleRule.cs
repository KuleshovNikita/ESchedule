using ESchedule.Domain.Lessons.Schedule;
using System.Text.Json;

namespace ESchedule.Business.ScheduleRules
{
    public abstract class BaseScheduleRule
    {
        public abstract string RuleName { get; }

        public Guid ActorId { get; set; }

        public abstract bool Verify(ScheduleModel schedule);

        public string GetJson() => JsonSerializer.Serialize(this);
    }
}
