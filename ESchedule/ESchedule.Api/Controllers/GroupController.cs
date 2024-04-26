using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class GroupController : BaseController<GroupModel>
    {
        public GroupController(IBaseService<GroupModel> groupService) : base(groupService) { }

        [Authorize]
        [HttpPost]
        public async Task CreateGroup([FromBody] GroupCreateModel groupModel)
            => await _service.CreateItem(groupModel);

        [Authorize]
        [HttpPut]
        public async Task UpdateGroup([FromBody] GroupUpdateModel groupModel)
            => await _service.UpdateItem(groupModel);

        [Authorize]
        [HttpGet("{groupId}")]
        public async Task<GroupModel> GetGroup(Guid groupId)
            => await _service.First(x => x.Id == groupId);

        [Authorize]
        [HttpDelete("{groupId}")]
        public async Task RemoveGroup(Guid groupId)
            => await _service.RemoveItem(groupId);
    }
}
