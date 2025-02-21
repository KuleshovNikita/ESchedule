using ESchedule.Api.Models.Requests;
using ESchedule.Business;
using ESchedule.Domain.ManyToManyModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers;

public class TeachersGroupsLessonsController(IBaseService<TeachersGroupsLessonsModel> service) : BaseController<TeachersGroupsLessonsModel>(service)
{
    [Authorize]
    [HttpPost]
    public async Task CreateItems([FromBody] IEnumerable<TeachersGroupsLessonsCreateModel> items)
        => await service.InsertMany(items);

    [Authorize]
    [HttpGet("{itemId}")]
    public async Task<TeachersGroupsLessonsModel> GetItems(Guid itemId) //TODO хз как получать про них инфу
        => await service.FirstOrDefault(x => x.Id == itemId);

    [Authorize]
    [HttpDelete]
    public async Task RemoveItems(TeachersGroupsLessonsModel itemModel)
        => await service.RemoveItem(itemModel);
}
