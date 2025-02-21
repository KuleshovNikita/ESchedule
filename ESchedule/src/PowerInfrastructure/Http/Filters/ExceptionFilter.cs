using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PowerInfrastructure.Exceptions;
using PowerInfrastructure.Extensions;
using System.Security.Authentication;

namespace PowerInfrastructure.Http.Filters;

public class ExceptionFilter(IHostEnvironment environment, ILogger<ExceptionFilter> logger) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception, "Unhandled error occured");

        var statusCode = context.Exception switch
        {
            ItemNotFoundException => 404,
            AuthenticationException => 401,
            _ => 500,
        };

        var details = new ProblemDetails()
        {
            Title = context.Exception!.Message,
            Detail = context.Exception!.Message,
            Status = statusCode
        };

        if (environment.IsLocal())
        {
            details.Extensions.Add("source", context.Exception.StackTrace);
        }

        context.Result = new ObjectResult(details);

        context.ExceptionHandled = true;
    }
}
