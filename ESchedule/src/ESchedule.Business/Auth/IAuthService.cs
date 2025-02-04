using ESchedule.Api.Models.Requests;
using ESchedule.Domain;
using ESchedule.Domain.Users;

namespace ESchedule.Business.Auth;

public interface IAuthService : IBaseService<UserModel>
{
    Task<Guid> ConfirmEmail(string key);

    Task<string> Login(AuthModel authModel);

    Task Register(UserCreateModel userModel);

    Task<UserModel> GetUserInfoWithTenant(Guid id);

    Task<UserModel> GetUserInfoWithoutTenant(Guid id);
}