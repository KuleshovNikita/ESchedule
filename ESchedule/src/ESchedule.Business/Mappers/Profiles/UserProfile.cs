using AutoMapper;
using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
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