using ESchedule.Api.Models.Requests.Create.TeachersLessons;
using ESchedule.Business;
using ESchedule.Domain.ManyToManyModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers;

public class TeachersLessonsController(IBaseService<TeachersLessonsModel> service) : BaseController<TeachersLessonsModel>(service)
{
    [Authorize]
    [HttpPost]
    public async Task CreateItems([FromBody] IEnumerable<TeachersLessonsCreateModel> items)
        => await service.InsertMany(items);

    [Authorize]
    [HttpGet("{itemId}")]
    public async Task<TeachersLessonsModel> GetItems(Guid itemId) //TODO хз как получать про них инфу
        => await service.FirstOrDefault(x => x.Id == itemId);

    [Authorize]
    [HttpDelete]
    public async Task RemoveItems(TeachersLessonsModel itemModel)
        => await service.RemoveItem(itemModel);
}
