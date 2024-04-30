using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Business.Users;
using ESchedule.Domain.Enums;
using ESchedule.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class UserController : BaseController<UserModel>
    {
        private readonly IUserService _userService;

        public UserController(IBaseService<UserModel> baseService, IUserService userService) : base(baseService) 
        {
            _userService = userService;
        }

        [Authorize]
        [HttpPut]
        public async Task UpdateUser([FromBody] UserUpdateModel userModel)
            => await _userService.UpdateUser(userModel);

        [Authorize]
        [HttpPatch("tenant")]
        public async Task UpdateUser(Guid userId)
            => await _userService.SignUserToTenant(userId);

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<UserModel> GetUsers(Guid userId)
            => await _service.FirstOrDefault(x => x.Id == userId);

        [Authorize]
        [HttpDelete("{userId}")]
        public async Task RemoveUser(Guid userId)
            => await _service.RemoveItem(userId);

        [Authorize]
        [HttpGet("role/{role}")]
        public async Task<IEnumerable<UserModel>> GetUsersByRole(Role role)
            => await _service.Where(x => x.Role == role);
    }
}
