using AutoMapper;
using ESchedule.Api.Models.Requests.Create.Groups;
using ESchedule.Api.Models.Updates;
using ESchedule.Domain.Users;

namespace ESchedule.Business.Mappers.Profiles;

public class GroupProfile : Profile
{
    public GroupProfile()
    {
        CreateMap<GroupCreateModel, GroupModel>();
        CreateMap<GroupUpdateModel, GroupModel>();
    }
}