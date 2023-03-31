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

        [Authorize]
        [HttpGet("{groupId}")]
        public async Task<ServiceResult<GroupModel>> GetGroup(Guid groupId)
            => await RunWithServiceResult(async () => await _service.First(x => x.Id == groupId));

        [Authorize]
        [HttpDelete("{groupId}")]
        public async Task<ServiceResult<Empty>> RemoveGroup(Guid groupId)
            => await RunWithServiceResult(async () => await _service.RemoveItem(groupId));
    }
}
