using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class GroupController : ResultingController<GroupModel>
    {
        public GroupController(IBaseService<GroupModel> groupService) : base(groupService) { }

        [Authorize]
        [HttpPost]
        public async Task<ServiceResult<Empty>> CreateGroup([FromBody] GroupCreateModel groupModel)
            => await RunWithServiceResult(async () => await _service.CreateItem(groupModel));

        [Authorize]
        [HttpPut]
        public async Task<ServiceResult<Empty>> UpdateGroup([FromBody] GroupUpdateModel groupModel)
            => await RunWithServiceResult(async () => await _service.UpdateItem(groupModel));

        // написать логику для выборки нескольки предметов вместо одного по айдишнику
        [Authorize]
        [HttpGet]
        public async Task<ServiceResult<Empty>> GetGroups() => throw new NotImplementedException();

        [Authorize]
        [HttpDelete]
        public async Task<ServiceResult<Empty>> RemoveGroup(Guid groupId)
            => await RunWithServiceResult(async () => await _service.RemoveItem(groupId));
    }
}
