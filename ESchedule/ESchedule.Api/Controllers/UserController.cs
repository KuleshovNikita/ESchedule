using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class UserController : ResultingController<UserModel>
    {
        public UserController(IBaseService<UserModel> userService) : base(userService) { }

        [Authorize]
        [HttpPut]
        public async Task<ServiceResult<Empty>> UpdateUser([FromBody] UserUpdateModel userModel)
            => await RunWithServiceResult(async () => await _service.UpdateItem(userModel));

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<ServiceResult<UserModel>> GetUsers(Guid userId)
            => await RunWithServiceResult(async () => await _service.First(x => x.Id == userId));

        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<ServiceResult<Empty>> RemoveUser(Guid userId)
            => await RunWithServiceResult(async () => await _service.RemoveItem(userId));
    }
}
