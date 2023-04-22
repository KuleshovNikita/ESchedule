using ESchedule.Business;
using ESchedule.Domain;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class LessonsParticipationController : ResultingController<BaseModel>
    {
        public LessonsParticipationController(IBaseService<BaseModel> service) : base(service)
        {
        }

        [Authorize]
        [HttpPatch("{pupilId}")]
        public async Task<ServiceResult<Empty>> TickParticipation(Guid pupilId)
            => throw new NotImplementedException();
    }
}
