using ESchedule.Api.Models.Requests.Update.Users;
using ESchedule.Business;
using ESchedule.Business.Users;
using ESchedule.Domain.Enums;
using ESchedule.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers;

public class UserController(IBaseService<UserModel> baseService, IUserService userService) : BaseController<UserModel>(baseService)
{
    [Authorize]
    [HttpPut]
    public async Task<UserModel> UpdateUser([FromBody] UserUpdateModel userModel)
        => await userService.UpdateUser(userModel);

    [Authorize]
    [HttpPatch("tenant")]
    public async Task UpdateUser(Guid userId)
        => await userService.SignUserToTenant(userId);

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<UserModel> GetUsers(Guid userId)
        => await service.FirstOrDefault(x => x.Id == userId);

    [Authorize]
    [HttpDelete("{userId}")]
    public async Task RemoveUser(Guid userId)
        => await service.RemoveItem(userId);

    [Authorize]
    [HttpGet("role/{role}")]
    public async Task<IEnumerable<UserModel>> GetUsersByRole(Role role)
        => await service.Where(x => x.Role == role);
}
