using ESchedule.Api.Models.Requests;
using ESchedule.Business;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class TeachersGroupsLessonsController : ResultingController<TeachersGroupsLessonsModel>
    {
        public TeachersGroupsLessonsController(IBaseService<TeachersGroupsLessonsModel> service) : base(service) { }

        [Authorize]
        [HttpPost]
        public async Task<ServiceResult<Empty>> CreateItems([FromBody] IEnumerable<TeachersGroupsLessonsCreateModel> items)
            => await RunWithServiceResult(async () => await _service.InsertMany(items));

        [Authorize]
        [HttpGet("{itemId}")]
        public async Task<ServiceResult<TeachersGroupsLessonsModel>> GetItems(Guid itemId) //TODO хз как получать про них инфу
            => await RunWithServiceResult(async () => await _service.First(x => x.Id == itemId));

        [Authorize]
        [HttpDelete]
        public async Task<ServiceResult<Empty>> RemoveItems(TeachersGroupsLessonsModel itemModel)
            => await RunWithServiceResult(async () => await _service.RemoveItem(itemModel));
    }
}
