using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class UserController : ResultingController<UserModel>
    {
        public UserController(IBaseService<UserModel> userService) : base(userService) { }

        [HttpPut]
        public async Task<ServiceResult<Empty>> UpdateUser([FromBody] UserUpdateModel userModel)
            => await RunWithServiceResult(async () => await _service.UpdateItem(userModel));

        // написать логику для выборки нескольки предметов вместо одного по айдишнику
        [HttpGet]
        public async Task<ServiceResult<Empty>> GetUsers() => throw new NotImplementedException();

        [HttpDelete]
        public async Task<ServiceResult<Empty>> RemoveUser(Guid userId)
            => await RunWithServiceResult(async () => await _service.RemoveItem(userId));
    }
}
