using AutoMapper;
using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
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
