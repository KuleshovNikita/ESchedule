using AutoMapper;
using ESchedule.Api.Models.Requests.Create.Lessons;
using ESchedule.Api.Models.Requests.Update.Lessons;
using ESchedule.Domain.Lessons;

namespace ESchedule.Business.Mappers.Profiles;

public class LessonProfile : Profile
{
    public LessonProfile()
    {
        CreateMap<LessonCreateModel, LessonModel>();
        CreateMap<LessonUpdateModel, LessonModel>();
    }
}