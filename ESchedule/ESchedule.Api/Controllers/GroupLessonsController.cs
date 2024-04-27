using ESchedule.Api.Models.Requests;
using ESchedule.Business;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class GroupLessonsController : BaseController<GroupsLessonsModel>
    {
        public GroupLessonsController(IBaseService<GroupsLessonsModel> service) : base(service) { }

        [Authorize]
        [HttpPost]
        public async Task CreateItems([FromBody] IEnumerable<GroupsLessonsCreateModel> items)
            => await _service.InsertMany(items);

        [Authorize]
        [HttpGet("{itemId}")]
        public async Task<GroupsLessonsModel> GetItems(Guid itemId) //TODO хз как получать про них инфу
            => await _service.First(x => x.Id == itemId);

        [Authorize]
        [HttpDelete]
        public async Task RemoveItems(GroupsLessonsModel itemModel)
            => await _service.RemoveItem(itemModel);
    }
}
