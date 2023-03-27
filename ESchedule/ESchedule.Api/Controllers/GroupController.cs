using ESchedule.Business;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class GroupController : ResultingController
    {
        private readonly BaseService<GroupModel> _groupService;

        public GroupController(BaseService<GroupModel> groupService) => _groupService = groupService;

        [HttpPost]
        public async Task<ServiceResult<Empty>> CreateGroup([FromBody] GroupModel groupModel)
            => await RunWithServiceResult(async () => await _groupService.CreateItem(groupModel));
    }
}
