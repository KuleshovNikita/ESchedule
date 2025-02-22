using AutoMapper;
using ESchedule.Api.Models.Requests.Create.Users;
using ESchedule.Api.Models.Requests.Update.Users;
using ESchedule.Domain.Users;

namespace ESchedule.Business.Mappers.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreateModel, UserModel>();
        CreateMap<UserUpdateModel, UserModel>();
        CreateMap<UserModel, UserUpdateModel>();
    }
}