using ESchedule.Api.Models.Requests;
using ESchedule.Domain;
using ESchedule.ServiceResulting;

namespace ESchedule.Business.Auth
{
    public interface IAuthService
    {
        Task<ServiceResult<Guid>> ConfirmEmail(string key);

        Task<string> Login(AuthModel authModel);

        Task<ServiceResult<Empty>> Register(UserCreateModel userModel);
    }
}
