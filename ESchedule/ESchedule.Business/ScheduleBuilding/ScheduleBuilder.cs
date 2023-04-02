using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.Schedule;
using ESchedule.Domain.Users;

namespace ESchedule.Business.ScheduleBuilding
{
    internal class ScheduleBuilder : IScheduleBuilder
    {
        private GroupModel _currentGroup = null!;
        private DayOfWeek _currentDay = DayOfWeek.Monday;
        private ScheduleBuilderHelpData _builderData = null!;
        private GroupLessonsManager _lessonsMananger = null!;
        private readonly List<ScheduleModel> _schedulesList = new List<ScheduleModel>();

        public void BuildSchedule(ScheduleBuilderHelpData builderData)
        {
            _builderData = builderData;

            foreach (var group in _builderData.AllTenantGroups)
            {
                _currentGroup = group;
                _lessonsMananger = new GroupLessonsManager(group.StudingLessons.Select(l => l.Lesson).ToArray());
                GenerateStudingWeek();
            }
        }

        private void GenerateStudingWeek()
        {
            for (var dayOfWeek = (int)DayOfWeek.Monday; dayOfWeek <= (int)DayOfWeek.Friday; dayOfWeek++)
            {
                _currentDay = (DayOfWeek)dayOfWeek;
                GenerateStudingDay();
            }
        }

        private void GenerateStudingDay()
        {
            var tenantSettings = _builderData.TargetTenant.Settings;
            var lessonStartTime = tenantSettings.StudyDayStartTime;

            for (var i = 0; i < _currentGroup.MaxLessonsCountPerDay; i++)
            {
                var lessonEndTime = AddLessonToSchedule(lessonStartTime, tenantSettings.LessonDurationTime);
                lessonStartTime = lessonEndTime + tenantSettings.BreaksDurationTime;
            }
        }

        private TimeSpan AddLessonToSchedule(TimeSpan lessonStartTime, TimeSpan lessonDurationTime)
        {
            var targetLesson = _lessonsMananger.Next();
            var teacher = GetSuitableTeacherByLesson(targetLesson.Id);

            var schedule = new ScheduleModel
            {
                StudyGroupId = _currentGroup.Id,
                TeacherId = teacher.Id,
                LessonId = targetLesson.Id,
                TenantId = _builderData.TargetTenant.Id,
                DayOfWeek = _currentDay,
                StartTime = lessonStartTime,
                EndTime = lessonStartTime + lessonDurationTime,
            };

            _schedulesList.Add(schedule);

            var lessonEndTime = lessonStartTime + lessonDurationTime;
            return lessonEndTime;
        }

        private UserModel GetSuitableTeacherByLesson(Guid lessonId)
        {
            var teacherLessonInfo = _currentGroup.GroupTeachersLessons.First(x => x.LessonId == lessonId);
            return teacherLessonInfo.Teacher;
        }
    }
}
