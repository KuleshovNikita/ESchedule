using ESchedule.Api.Models.Requests.Create.Tenants;
using ESchedule.Api.Models.Requests.Create.Tenants.RequestAccess;
using ESchedule.Business.Users;
using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.Auth;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PowerInfrastructure.AutoMapper;
using PowerInfrastructure.Http;
using System.Security.Claims;

namespace ESchedule.Business.Tenant;

public class TenantService(
    IRepository<TenantModel> repository,
    IRepository<RequestTenantAccessModel> tenantRequestRepo,
    IAuthRepository authService,
    IMainMapper mapper,
    IUserService userService,
    IClaimsAccessor claimAccessor,
    ITenantContextProvider tenantContextProvider,
    IRepository<UserModel> userRepo,
    ILogger<TenantService> logger
)
    : BaseService<TenantModel>(repository, mapper), ITenantService
{
    public async Task<TenantModel> CreateTenant(TenantCreateModel request)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Creating a new tenant");

        var existingTenant = await Repository.SingleOrDefault(x => EF.Functions.Like(x.Name, $"{request.Name}"));

        if (existingTenant != null)
        {
            logger.LogWarning("Cannot create tenant because it already exists");
            throw new InvalidOperationException(Resources.TenantAlreadyExists);
        }

        var userId = claimAccessor.GetRequiredClaimValue(ClaimTypes.NameIdentifier);
        var user = await authService.SingleOrDefault(x => x.Id == Guid.Parse(userId));

        if (user.TenantId != null)
        {
            logger.LogWarning("User {id} already belongs to a tenant", userId);
            throw new InvalidOperationException(Resources.UserAlreadyBlongsToTenant);
        }
        var newTenant = await CreateItem(request);

        await userService.SignUserToTenant(user.Id, newTenant.Id);

        logger.LogInformation("Successfully created a new tenant for user {id}", userId);

        return newTenant;
    }

    public async Task AcceptAccessRequest(Guid userId)
    {
        logger.LogInformation("Trying to accept tenant access request for user {id}", userId);

        var user = await userRepo.IgnoreQueryFilters().SingleOrDefault(x => x.Id == userId)
            ?? throw new EntityNotFoundException(Resources.UserDoesNotExist);

        var allUserRequests = await tenantRequestRepo.IgnoreQueryFilters().Where(x => x.UserId == userId);
        await tenantRequestRepo.RemoveRange(allUserRequests);
        user.TenantId = tenantContextProvider.Current.TenantId;

        await userRepo.SaveChangesAsync();

        logger.LogInformation("Successfully accepted user {userId} access request for tenant {tenantId}", userId, user.TenantId);
    }

    public async Task DeclineAccessRequest(Guid userId)
    {
        logger.LogInformation("Trying to decline tenant access request for user {id}", userId);

        var user = await userRepo.IgnoreQueryFilters().SingleOrDefault(x => x.Id == userId)
            ?? throw new EntityNotFoundException(Resources.UserDoesNotExist);

        var userRequest = await tenantRequestRepo.SingleOrDefault(x => x.UserId == userId);
        await tenantRequestRepo.Remove(userRequest);

        await userRepo.SaveChangesAsync();

        logger.LogInformation("Successfully declined user {userId} access request for tenant {tenantId}", userId, user.TenantId);
    }

    public async Task<IEnumerable<UserModel>> GetAccessRequests()
    {
        var tenantId = tenantContextProvider.Current.TenantId;
        logger.LogInformation("Getting access requests for tenant {tenantId}", tenantId);

        _ = await Repository.SingleOrDefault(x => x.Id == tenantId)
            ?? throw new EntityNotFoundException(Resources.TenantDoesNotExist);

        var requests = await tenantRequestRepo.All();
        var userIds = requests.Select(x => x.UserId);

        return await userRepo.IgnoreQueryFilters().Where(x => userIds.Contains(x.Id));
    }

    public async Task RequestTenantAccess(RequestTenantAccessCreateModel request)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Requesting tenant {tenantId} access for user {userId}", request.TenantId, request.UserId);

        _ = await Repository.SingleOrDefault(x => x.Id == request.TenantId)
                ?? throw new EntityNotFoundException(Resources.TenantDoesNotExist);

        var entity = await tenantRequestRepo.SingleOrDefault(x => x.UserId == request.UserId);

        if (entity != null)
        {
            logger.LogWarning("User {userId} already sent tenant request access", request.UserId);
            throw new InvalidOperationException(Resources.RequestToTenantAlreadySent);
        }

        var domainModel = Mapper.Map<RequestTenantAccessModel>(request);

        await tenantRequestRepo.Insert(domainModel);

        logger.LogInformation("User {userId} sent access request to tenant {tenantId} successfully", request.UserId, request.TenantId);
    }
}