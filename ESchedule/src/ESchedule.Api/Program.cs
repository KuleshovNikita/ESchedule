using ESchedule.Startup.Extensions;
using Microsoft.EntityFrameworkCore;
using PowerInfrastructure.DependencyInjection.Extensions;
using PowerInfrastructure.Extensions;
using PowerInfrastructure.Http.Filters;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services.AddLogging()
                .AddEndpointsApiExplorer()
                .AddControllers(opt =>
                {
                    opt.Filters.Add<ExceptionFilter>();
                })
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });

builder.Services.AddDevelopmentServices(environment)
                .RegisterDependencyModules()
                .AddAutoMappers()
                .AddHttpContextAccessor()
                .AddClaimsAccessor()
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var migratedSuccessfully = await app.Migrate();
if(migratedSuccessfully)
{
    app.Run();
}



