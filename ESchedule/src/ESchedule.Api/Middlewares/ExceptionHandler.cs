using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Middlewares;

public class ExceptionHandler
{
    public async Task InvokeAsync(HttpContext context)
    {
        var exception = context.Features.Get<IExceptionHandlerPathFeature>();

        var details = new ProblemDetails()
        {
            Detail = exception!.Error.Message,
            Status = context.Response.StatusCode,
            Instance = exception.Path
        };

        details.Extensions.Add("Source", exception.Error.StackTrace);

        var response = Results.Problem(details);
        await response.ExecuteAsync(context);
    }
}
