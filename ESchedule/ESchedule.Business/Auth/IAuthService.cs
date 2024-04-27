using ESchedule.Api.Models.Requests;
using ESchedule.Domain;

namespace ESchedule.Business.Auth
{
    public interface IAuthService
    {
        Task<Guid> ConfirmEmail(string key);

        Task<string> Login(AuthModel authModel);

        Task Register(UserCreateModel userModel);
    }
}
