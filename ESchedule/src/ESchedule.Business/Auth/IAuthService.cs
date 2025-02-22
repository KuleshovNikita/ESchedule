using ESchedule.Api.Models.Requests.Create.Users;
using ESchedule.Domain;
using ESchedule.Domain.Users;
using System.Security.Claims;

namespace ESchedule.Business.Auth;

public interface IAuthService : IBaseService<UserModel>
{
    Task<Guid> ConfirmEmail(string key);

    Task<string> Login(AuthModel authModel);

    Task Register(UserCreateModel userModel);

    Task<UserModel?> GetAuthenticatedUserInfo(IEnumerable<Claim>? claims);
}