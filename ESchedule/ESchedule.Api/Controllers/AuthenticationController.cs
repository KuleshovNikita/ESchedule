using ESchedule.Api.Models.Requests;
using ESchedule.Business;
using ESchedule.Business.Auth;
using ESchedule.Domain;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ESchedule.Api.Controllers
{
    public class AuthenticationController : ResultingController<UserModel>
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService, IBaseService<UserModel> userService) : base(userService)
            => _authService = authService;

        [HttpPost("register")]
        public async Task<ServiceResult<Empty>> Register([FromBody] UserCreateModel registerModel)
            => await RunWithServiceResult(async () => await _authService.Register(registerModel));

        [HttpPost("login")]
        public async Task<string> Login([FromBody] AuthModel authModel)
            => await _authService.Login(authModel);

        [HttpGet]
        public async Task<ServiceResult<UserModel>> GetAuthenticatedUserInfo()
        {
            var claims = HttpContext.User.Claims;

            if(!claims.Any())
            {
                return new ServiceResult<UserModel>();
            }

            var userId = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if(!Guid.TryParse(userId, out var id))
            {
                return new ServiceResult<UserModel>().Fail("Invalid token provided");
            }

            return await RunWithServiceResult(async () => await _service.First(x => x.Id == id));
        }

        [HttpPatch("confirmEmail/{key}")]
        public async Task<ServiceResult<Guid>> ConfirmEmail(string key)
            => await RunWithServiceResult(async () =>
            {
                key = Uri.UnescapeDataString(key);
                return await _authService.ConfirmEmail(key);
            });
    }
}
