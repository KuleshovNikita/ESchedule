using ESchedule.Api.Models.Requests;
using ESchedule.Business;
using ESchedule.Domain.ManyToManyModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class TeachersGroupsLessonsController : BaseController<TeachersGroupsLessonsModel>
    {
        public TeachersGroupsLessonsController(IBaseService<TeachersGroupsLessonsModel> service) : base(service) { }

        [Authorize]
        [HttpPost]
        public async Task CreateItems([FromBody] IEnumerable<TeachersGroupsLessonsCreateModel> items)
            => await _service.InsertMany(items);

        [Authorize]
        [HttpGet("{itemId}")]
        public async Task<TeachersGroupsLessonsModel> GetItems(Guid itemId) //TODO хз как получать про них инфу
            => await _service.First(x => x.Id == itemId);

        [Authorize]
        [HttpDelete]
        public async Task RemoveItems(TeachersGroupsLessonsModel itemModel)
            => await _service.RemoveItem(itemModel);
    }
}
