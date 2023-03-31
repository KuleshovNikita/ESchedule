using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.Schedule;
using ESchedule.Domain.Users;

namespace ESchedule.Business.ScheduleBuilding
{
    internal class ScheduleBuilder : IScheduleBuilder
    {
        private GroupModel _currentGroup = null!;
        private ScheduleBuilderHelpData _builderData = null!;
        private readonly List<ScheduleModel> _schedulesList = new List<ScheduleModel>();

        public void BuildSchedule(ScheduleBuilderHelpData builderData)
        {
            _builderData = builderData;

            foreach (var group in _builderData.AllTenantGroups)
            {
                _currentGroup = group;
                GenerateStudingWeek();
            }
        }

        private void GenerateStudingWeek()
        {
            for (var dayOfWeek = (int)DayOfWeek.Monday; dayOfWeek <= (int)DayOfWeek.Friday; dayOfWeek++)
            {
                GenerateStudingDay((DayOfWeek)dayOfWeek);
            }
        }

        private void GenerateStudingDay(DayOfWeek currentDayOfWeek)
        {
            var tenantSettings = _builderData.TargetTenant.Settings;
            var dayTime = tenantSettings.StudyDayStartTime;

            for (var lessonNumber = 0; lessonNumber < _currentGroup.MaxLessonsCountPerDay; lessonNumber++)
            {
                var targetLessonIndex = new Random().Next(0, _currentGroup.StudingLessons.Count);

                var schedule = new ScheduleModel
                {
                    StudyGroupId = _currentGroup.Id,
                    TeacherId = Guid.NewGuid(), // заменить на учителя подходящего для этой группы
                    LessonId = _currentGroup.StudingLessons[targetLessonIndex].Id,
                    TenantId = _builderData.TargetTenant.Id,
                    DayOfWeek = currentDayOfWeek,
                    StartTime = dayTime,
                    EndTime = dayTime + tenantSettings.LessonDurationTime,
                };
            }
        }
    }
}
