using ESchedule.Api.Models.Requests;
using ESchedule.Business;
using ESchedule.Business.Auth;
using ESchedule.Domain;
using ESchedule.Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers;

public class AuthenticationController(IAuthService authService, IBaseService<UserModel> userService) : BaseController<UserModel>(userService)
{
    [HttpPost("register")]
    public async Task Register([FromBody] UserCreateModel registerModel)
        => await authService.Register(registerModel);

    [HttpPost("login")]
    public async Task<string> Login([FromBody] AuthModel authModel)
        => await authService.Login(authModel);

    [HttpGet]
    public async Task<UserModel?> GetAuthenticatedUserInfo()
        => await authService.GetAuthenticatedUserInfo(HttpContext.User.Claims);

    [HttpPatch("confirmEmail/{key}")]
    public async Task<Guid> ConfirmEmail(string key)
    {
        key = Uri.UnescapeDataString(key);
        return await authService.ConfirmEmail(key);
    }
}
