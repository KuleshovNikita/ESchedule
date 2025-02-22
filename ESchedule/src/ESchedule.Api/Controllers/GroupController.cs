using ESchedule.Api.Models.Requests.Create.Groups;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers;

public class GroupController(IBaseService<GroupModel> groupService) : BaseController<GroupModel>(groupService)
{
    [Authorize]
    [HttpPost]
    public async Task CreateGroup([FromBody] GroupCreateModel groupModel)
        => await service.CreateItem(groupModel);

    [Authorize]
    [HttpPut]
    public async Task UpdateGroup([FromBody] GroupUpdateModel groupModel)
        => await service.UpdateItem(groupModel);

    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<GroupModel>> GetGroups()
        => await service.GetItems();

    [Authorize]
    [HttpGet("{groupId}")]
    public async Task<GroupModel> GetGroup(Guid groupId)
        => await service.FirstOrDefault(x => x.Id == groupId);

    [Authorize]
    [HttpDelete("{groupId}")]
    public async Task RemoveGroup(Guid groupId)
        => await service.RemoveItem(groupId);
}
