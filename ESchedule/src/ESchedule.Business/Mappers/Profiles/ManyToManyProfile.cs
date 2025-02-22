using AutoMapper;
using ESchedule.Api.Models.Requests.Create.GroupsLessons;
using ESchedule.Api.Models.Requests.Create.TeachersGroupsLessons;
using ESchedule.Api.Models.Requests.Create.TeachersLessons;
using ESchedule.Domain.ManyToManyModels;

namespace ESchedule.Business.Mappers.Profiles;

public class ManyToManyProfile : Profile
{
    public ManyToManyProfile()
    {
        CreateMap<TeachersGroupsLessonsCreateModel, TeachersGroupsLessonsModel>();
        CreateMap<TeachersLessonsCreateModel, TeachersLessonsModel>();
        CreateMap<GroupsLessonsCreateModel, GroupsLessonsModel>();
    }
}