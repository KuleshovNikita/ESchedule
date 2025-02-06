using ESchedule.Api.Models.Requests;
using ESchedule.Business.Mappers;
using ESchedule.Business.Users;
using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.Auth;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ESchedule.Business.Tenant;

public class TenantService(
    IRepository<TenantModel> repository,
    IRepository<TenantSettingsModel> settingsRepo,
    IRepository<RequestTenantAccessModel> tenantRequestRepo,
    IAuthRepository authService,
    IMainMapper mapper,
    IUserService userService,
    IHttpContextAccessor httpAccessor,
    ITenantContextProvider tenantContextProvider,
    IRepository<UserModel> userRepo
)
    : BaseService<TenantModel>(repository, mapper), ITenantService
{
    public async Task<TenantModel> CreateTenant(TenantCreateModel request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var tenantExists = await Repository.SingleOrDefault(x => EF.Functions.Like(x.Name, $"{request.Name}"));

        if (tenantExists != null)
        {
            throw new InvalidOperationException(Resources.TenantAlreadyExists);
        }

        var userId = httpAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
        var user = await authService.SingleOrDefault(x => x.Id == Guid.Parse(userId));

        if (user.TenantId != null)
        {
            throw new InvalidOperationException(Resources.UserAlreadyBlongsToTenant);
        }
        var tenant = await CreateItem(request);

        await userService.SignUserToTenant(user.Id, tenant.Id);

        return tenant;
    }

    public async Task AcceptAccessRequest(Guid userId)
    {
        var user = await userRepo.IgnoreQueryFilters().SingleOrDefault(x => x.Id == userId)
            ?? throw new EntityNotFoundException(Resources.UserDoesNotExist);

        var allUserRequests = await tenantRequestRepo.IgnoreQueryFilters().Where(x => x.UserId == userId);
        await tenantRequestRepo.RemoveRange(allUserRequests);
        user.TenantId = tenantContextProvider.Current.TenantId;

        await userRepo.SaveChangesAsync();
    }

    public async Task DeclineAccessRequest(Guid userId)
    {
        var user = await userRepo.IgnoreQueryFilters().SingleOrDefault(x => x.Id == userId)
            ?? throw new EntityNotFoundException(Resources.UserDoesNotExist);

        var userRequest = await tenantRequestRepo.SingleOrDefault(x => x.UserId == userId);
        await tenantRequestRepo.Remove(userRequest);

        await userRepo.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserModel>> GetAccessRequests()
    {
        var tenantExists = await Repository.Any(x => x.Id == tenantContextProvider.Current.TenantId);

        if (!tenantExists)
        {
            throw new EntityNotFoundException(Resources.TenantDoesNotExist);
        }

        var requests = await tenantRequestRepo.All();
        var userIds = requests.Select(x => x.UserId);

        return await userRepo.IgnoreQueryFilters().Where(x => userIds.Contains(x.Id));
    }

    public async Task RequestTenantAccess(RequestTenantAccessCreateModel request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var tenantExists = await Repository.Any(x => x.Id == request.TenantId);

        if (!tenantExists)
        {
            throw new EntityNotFoundException(Resources.TenantDoesNotExist);
        }

        var entity = await tenantRequestRepo.SingleOrDefault(x => x.UserId == request.UserId);

        if (entity != null)
        {
            throw new InvalidOperationException(Resources.RequestToTenantAlreadySent);
        }

        var domainModel = Mapper.Map<RequestTenantAccessModel>(request);

        await tenantRequestRepo.Insert(domainModel);
    }

    public async Task<TenantSettingsModel> CreateTenantSettings(TenantSettingsModel request)
        => await settingsRepo.Insert(request);
}