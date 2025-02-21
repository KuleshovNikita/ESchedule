using ESchedule.Api.Models.Requests;
using ESchedule.Business;
using ESchedule.Domain.ManyToManyModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers;

public class GroupLessonsController(IBaseService<GroupsLessonsModel> service) : BaseController<GroupsLessonsModel>(service)
{
    [Authorize]
    [HttpPost]
    public async Task CreateItems([FromBody] IEnumerable<GroupsLessonsCreateModel> items)
        => await service.InsertMany(items);

    [Authorize]
    [HttpGet("{itemId}")]
    public async Task<GroupsLessonsModel> GetItems(Guid itemId) //TODO хз как получать про них инфу
        => await service.FirstOrDefault(x => x.Id == itemId);

    [Authorize]
    [HttpDelete]
    public async Task RemoveItems(GroupsLessonsModel itemModel)
        => await service.RemoveItem(itemModel);
}
