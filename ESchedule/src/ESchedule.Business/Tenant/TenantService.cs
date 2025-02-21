using ESchedule.Api.Models.Requests;
using ESchedule.Business.Users;
using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.Auth;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using Microsoft.EntityFrameworkCore;
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
    IRepository<UserModel> userRepo
)
    : BaseService<TenantModel>(repository, mapper), ITenantService
{
    public async Task<TenantModel> CreateTenant(TenantCreateModel request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var existingTenant = await Repository.SingleOrDefault(x => EF.Functions.Like(x.Name, $"{request.Name}"));

        if (existingTenant != null)
        {
            throw new InvalidOperationException(Resources.TenantAlreadyExists);
        }

        var userId = claimAccessor.GetRequiredClaimValue(ClaimTypes.NameIdentifier);
        var user = await authService.SingleOrDefault(x => x.Id == Guid.Parse(userId));

        if (user.TenantId != null)
        {
            throw new InvalidOperationException(Resources.UserAlreadyBlongsToTenant);
        }
        var newTenant = await CreateItem(request);

        await userService.SignUserToTenant(user.Id, newTenant.Id);

        return newTenant;
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
        _ = await Repository.SingleOrDefault(x => x.Id == tenantContextProvider.Current.TenantId)
            ?? throw new EntityNotFoundException(Resources.TenantDoesNotExist);

        var requests = await tenantRequestRepo.All();
        var userIds = requests.Select(x => x.UserId);

        return await userRepo.IgnoreQueryFilters().Where(x => userIds.Contains(x.Id));
    }

    public async Task RequestTenantAccess(RequestTenantAccessCreateModel request)
    {
        ArgumentNullException.ThrowIfNull(request);

        _ = await Repository.SingleOrDefault(x => x.Id == request.TenantId)
                ?? throw new EntityNotFoundException(Resources.TenantDoesNotExist);

        var entity = await tenantRequestRepo.SingleOrDefault(x => x.UserId == request.UserId);

        if (entity != null)
        {
            throw new InvalidOperationException(Resources.RequestToTenantAlreadySent);
        }

        var domainModel = Mapper.Map<RequestTenantAccessModel>(request);

        await tenantRequestRepo.Insert(domainModel);
    }
}