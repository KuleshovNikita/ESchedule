using ESchedule.Business;
using ESchedule.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TModel> : ControllerBase
        where TModel : BaseModel
    {
        protected readonly IBaseService<TModel> _service;

        public BaseController(IBaseService<TModel> service)
        {
            _service = service;
        }
    }
}
