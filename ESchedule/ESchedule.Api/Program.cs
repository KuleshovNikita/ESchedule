using ESchedule.Api.Filters;
using ESchedule.Api.Middlewares;
using ESchedule.DataAccess.Context;
using ESchedule.Domain.Auth;
using ESchedule.Startup.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers(opt =>
                {
                    opt.Filters.Add<TenantContextFilter>();
                })
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cfg =>
{
    cfg.ResolveConflictingActions(api => api.First());
    cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Name = "Authorization",
        In = ParameterLocation.Header
    });
    cfg.AddSecurityRequirement(new OpenApiSecurityRequirement 
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
builder.Services.RegisterDependencies();
builder.Services.AddCors(x => x.AllowAnyOriginPolicy());
builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureAuthentication(configuration.GetSection("Jwt").Get<JwtSettings>()!);
builder.Services.ConfigureAuthorization();
builder.Services.ConfigureDbConnection(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
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
context.Database.Migrate();

app.Run();
