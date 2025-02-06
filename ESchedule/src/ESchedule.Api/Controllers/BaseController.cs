using ESchedule.Business;
using ESchedule.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController<TModel>(IBaseService<TModel> service) : ControllerBase
    where TModel : BaseModel
{
    protected readonly IBaseService<TModel> service = service;
}
