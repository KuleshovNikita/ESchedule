using ESchedule.Api.Middlewares;
using ESchedule.DataAccess.Context;
using ESchedule.Startup.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services.AddLogging()
                .AddControllers()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });

builder.Services.AddDevelopmentServices(environment)
                .AddEndpointsApiExplorer()
                .RegisterDependencyModules()
                .AddAutoMappers()
                .AddHttpContextAccessor()
                .AddDbContext(configuration)
                .AddCustomAuthentication(configuration)
                .AddCustomAuthorization();

var app = builder.Build();

if (environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseExceptionHandler(new ExceptionHandlerOptions() { ExceptionHandler = new ExceptionHandler().InvokeAsync });

app.UseAuthentication();
app.UseAuthorization();

//app.UseMiddleware<ErrorResponseMiddleware>();

app.MapControllers();

var options = new DbContextOptionsBuilder<EScheduleDbContext>().UseSqlServer(configuration.GetConnectionString("SqlServer")!);
using var context = new EScheduleDbContext(options.Options);
await context.Database.MigrateAsync();


app.Run();
