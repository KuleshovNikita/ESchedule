using ESchedule.Api.Models.Requests;
using ESchedule.Business;
using ESchedule.Domain.ManyToManyModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class TeachersLessonsController : BaseController<TeachersLessonsModel>
    {
        public TeachersLessonsController(IBaseService<TeachersLessonsModel> service) : base(service) { }

        [Authorize]
        [HttpPost]
        public async Task CreateItems([FromBody] IEnumerable<TeachersLessonsCreateModel> items)
            => await _service.InsertMany(items);

        [Authorize]
        [HttpGet("{itemId}")]
        public async Task<TeachersLessonsModel> GetItems(Guid itemId) //TODO хз как получать про них инфу
            => await _service.FirstOrDefault(x => x.Id == itemId);

        [Authorize]
        [HttpDelete]
        public async Task RemoveItems(TeachersLessonsModel itemModel)
            => await _service.RemoveItem(itemModel);
    }
}
