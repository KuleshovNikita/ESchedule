using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using PowerInfrastructure.Exceptions;
using PowerInfrastructure.Extensions;
using System.Security.Authentication;

namespace PowerInfrastructure.Http.Filters;

public class ExceptionFilter(IHostEnvironment environment) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
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
