using ESchedule.Business.ScheduleRules;
using ESchedule.Domain.Lessons;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.Schedule;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.Domain.Users;

namespace ESchedule.Business.ScheduleBuilding
{
    internal class ScheduleBuilder : IScheduleBuilder
    {
        private GroupModel _currentGroup = null!;
        private DayOfWeek _currentDay = DayOfWeek.Monday;
        private IEnumerable<BaseScheduleRule> _rules = null!;
        private ScheduleBuilderHelpData _builderData = null!;
        private GroupLessonsManager _lessonsMananger = null!;
        private readonly HashSet<ScheduleModel> _schedulesSet = new HashSet<ScheduleModel>();

        public HashSet<ScheduleModel> BuildSchedules(ScheduleBuilderHelpData builderData, IEnumerable<BaseScheduleRule> rules)
        {
            _builderData = builderData;
            _rules = rules;

            foreach (var group in _builderData.AllTenantGroups)
            {
                _currentGroup = group;
                _lessonsMananger = new GroupLessonsManager(group.StudingLessons.Select(l => l.Lesson).ToArray());
                GenerateStudingWeek();
            }

            return SimplifySchedulesSet(_schedulesSet);
        }

        private HashSet<ScheduleModel> SimplifySchedulesSet(HashSet<ScheduleModel> schedulesSet)
        {
            foreach(var schedule in schedulesSet)
            {
                schedule.Teacher = null!;
                schedule.StudyGroup = null!;
                schedule.Lesson = null!;
                schedule.Tenant = null!;
            }

            return schedulesSet;
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
            (var teacher, var lesson) = FindFreeTeacher(lessonStartTime);

            if(teacher != null && lesson != null)
            {
                var schedule = new ScheduleModel
                {
                    Id = Guid.NewGuid(),
                    StudyGroupId = _currentGroup.Id,
                    TeacherId = teacher!.Id,
                    LessonId = lesson!.Id,
                    TenantId = _builderData.TargetTenant.Id,
                    DayOfWeek = _currentDay,
                    StartTime = lessonStartTime,
                    EndTime = lessonStartTime + lessonDurationTime,
                };

                if(RulesVerified(schedule))
                {
                    _schedulesSet.Add(schedule);
                }
            }

            var lessonEndTime = lessonStartTime + lessonDurationTime;
            return lessonEndTime;
        }

        private (UserModel?, LessonModel?) FindFreeTeacher(TimeSpan lessonStartTime)
        {
            for (var i = 0; i < _lessonsMananger.Count; i++)
            {
                var targetLesson = _lessonsMananger.Next();
                var teacher = GetSuitableTeacherByLesson(targetLesson.Id);

                if (!IsTeacherBusyAtThisTime(teacher, lessonStartTime))
                {
                    return (teacher, targetLesson);
                }
            }

            return (null, null);
        }

        private bool IsTeacherBusyAtThisTime(UserModel teacher, TimeSpan lessonStartTime)
            => teacher.StudySchedules.Any(sch => sch.StartTime == lessonStartTime && sch.DayOfWeek == _currentDay);

        private UserModel GetSuitableTeacherByLesson(Guid lessonId)
        {
            var teacherLessonInfo = _currentGroup.GroupTeachersLessons.First(x => x.LessonId == lessonId);
            return teacherLessonInfo.Teacher;
        }

        private bool RulesVerified(ScheduleModel schedule)
        {
            foreach(var rule in _rules)
            {
                if(!rule.Verify(schedule))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
