using ESchedule.Api.Models.Requests.Update.Users;
using ESchedule.Business.Hashing;
using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.Auth;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using Microsoft.Extensions.Logging;
using PowerInfrastructure.AutoMapper;

namespace ESchedule.Business.Users;

public class UserService(
    IRepository<UserModel> repository, 
    IMainMapper mapper, 
    IPasswordHasher passwordHasher,
    ITenantContextProvider tenantContextProvider, 
    IAuthRepository authRepository,
    ILogger<UserService> logger
)
    : BaseService<UserModel>(repository, mapper), IUserService
{
    public async Task SignUserToTenant(Guid userId)
        => await SignUserToTenant(userId, tenantContextProvider.Current.TenantId);

    public async Task SignUserToTenant(Guid userId, Guid tenantId)
    {
        logger.LogInformation("Signing user {userId} to tenant {tenantId}", userId, tenantId);

        var user = await authRepository.SingleOrDefault(x => x.Id == userId)
            ?? throw new EntityNotFoundException(Resources.NoUsersForSpecifiedKeyWereFound);

        user.TenantId = tenantId;
        await authRepository.SaveChangesAsync();

        logger.LogInformation("Successfully signed user {userId} to tenant {tenantId}", userId, tenantId);
    }

    public async Task<UserModel> UpdateUser(UserUpdateModel updateModel)
    {
        logger.LogInformation("Updating information for user {userId}", updateModel.Id);

        var isPasswordChanged = updateModel.Password != null;

        var user = await SingleOrDefault(x => x.Id == updateModel.Id)
            ?? throw new EntityNotFoundException();

        user = Mapper.MapOnlyUpdatedProperties(updateModel, user);

        if (isPasswordChanged)
        {
            user.Password = passwordHasher.HashPassword(updateModel.Password!);
        }

        await Repository.Update(user);

        logger.LogInformation("Updated user data successfully");

        return user;
    }
}