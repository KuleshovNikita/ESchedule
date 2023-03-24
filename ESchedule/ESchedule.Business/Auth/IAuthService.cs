using ESchedule.Api.Models.Requests;
using ESchedule.Domain;
using ESchedule.ServiceResulting;

namespace ESchedule.Business.Auth
{
    public interface IAuthService
    {
        Task<ServiceResult<string>> ConfirmEmail(string key);

        Task<ServiceResult<string>> Login(AuthModel authModel);

        Task<ServiceResult<string>> Register(UserRequestModel userModel);
    }
}
