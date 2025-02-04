using ESchedule.Api.Models.Requests;
using ESchedule.Business;
using ESchedule.Business.Auth;
using ESchedule.Domain;
using ESchedule.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ESchedule.Api.Controllers
{
    public class AuthenticationController : BaseController<UserModel>
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService, IBaseService<UserModel> userService) : base(userService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task Register([FromBody] UserCreateModel registerModel)
            => await _authService.Register(registerModel);

        [HttpPost("login")]
        public async Task<string> Login([FromBody] AuthModel authModel)
            => await _authService.Login(authModel);

        [HttpGet]
        public async Task<UserModel> GetAuthenticatedUserInfo()
        {
            var claims = HttpContext.User.Claims;

            if(!claims.Any())
            {
                return null!;
            }

            var userId = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var hasTenant = claims.Any(x => x.Type == ClaimTypes.Surname);

            if(!Guid.TryParse(userId, out var id))
            {
                throw new InvalidOperationException("Invalid token provided");
            }

            return hasTenant
                    ? await _authService.GetUserInfoWithTenant(id)
                    : await _authService.GetUserInfoWithoutTenant(id);
        }

        [HttpPatch("confirmEmail/{key}")]
        public async Task<Guid> ConfirmEmail(string key)
        {
            key = Uri.UnescapeDataString(key);
            return await _authService.ConfirmEmail(key);
        }
    }
}
