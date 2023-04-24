using ESchedule.Domain;
using ESchedule.Domain.Lessons.Schedule;
using System.Text.Json;

namespace ESchedule.Business.ScheduleRules
{
    public class BaseScheduleRule
    {
        public virtual string Name { get; set; } = null!;

        public Guid ActorId { get; set; }

        public virtual bool Verify(ScheduleModel schedule) => throw new NotImplementedException();

        public virtual string GetJson() => JsonSerializer.Serialize(this, GetType());
    }
}
