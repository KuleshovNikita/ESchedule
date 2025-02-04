using AutoMapper;
using ESchedule.Api.Models.Requests;
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