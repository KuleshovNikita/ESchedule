using ESchedule.Domain.Lessons;

namespace ESchedule.Business.ScheduleBuilding;

public class GroupLessonsManager(LessonModel[] lessonsList)
{
    public int Count => lessonsList.Length;

    private int _currentLessonIndex = 0;

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

    private LessonModel GetNextLesson() => lessonsList[_currentLessonIndex++];
}
