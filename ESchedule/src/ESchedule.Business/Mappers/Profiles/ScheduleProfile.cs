using AutoMapper;
using ESchedule.Api.Models.Requests.Create.Schedules;
using ESchedule.Api.Models.Requests.Create.Schedules.Rules;
using ESchedule.Api.Models.Requests.Update.Schedules;
using ESchedule.Api.Models.Responses;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.Schedule.Rules;

namespace ESchedule.Business.Mappers.Profiles;

public class SchedulesProfile : Profile
{
    public SchedulesProfile()
    {
        CreateMap<ScheduleUpdateModel, ScheduleModel>();
        CreateMap<ScheduleCreateModel, ScheduleModel>();
        CreateMap<ScheduleModel, ScheduleItemResponse>()
            .ForPath(x => x.Teacher.Name, o => o.MapFrom(x => x.Teacher.Name))
            .ForPath(x => x.Teacher.LastName, o => o.MapFrom(x => x.Teacher.LastName))
            .ForPath(x => x.Teacher.FatherName, o => o.MapFrom(x => x.Teacher.FatherName))
            .ForMember(x => x.GroupName, o => o.MapFrom(x => x.StudyGroup.Title))
            .ForMember(x => x.LessonName, o => o.MapFrom(x => x.Lesson.Title));

        CreateMap<RuleCreateModel, RuleModel>();
    }
}