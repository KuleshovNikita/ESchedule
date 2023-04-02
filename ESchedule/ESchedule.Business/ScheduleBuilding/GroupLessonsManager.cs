using ESchedule.Domain.Lessons;

namespace ESchedule.Business.ScheduleBuilding
{
    public class GroupLessonsManager
    {
        private readonly LessonModel[] _lessonsList;
        private int _currentLessonIndex = 0;

        public GroupLessonsManager(LessonModel[] lessonsList)
        {
            _lessonsList = lessonsList;
        }

        public LessonModel Next()
        {
            try
            {
                return GetNextLesson();
            }
            catch(IndexOutOfRangeException)
            {
                _currentLessonIndex = 0;
                return GetNextLesson();
            }
        }

        private LessonModel GetNextLesson() => _lessonsList[_currentLessonIndex++];
    }
}
