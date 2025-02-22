using ESchedule.Api.Models.Requests.Update.Users;
using ESchedule.Business.Hashing;
using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.Auth;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using PowerInfrastructure.AutoMapper;

namespace ESchedule.Business.Users;

public class UserService(
    IRepository<UserModel> repository, 
    IMainMapper mapper, 
    IPasswordHasher passwordHasher,
    ITenantContextProvider tenantContextProvider, 
    IAuthRepository authRepository
)
    : BaseService<UserModel>(repository, mapper), IUserService
{
    public async Task SignUserToTenant(Guid userId)
        => await SignUserToTenant(userId, tenantContextProvider.Current.TenantId);

    public async Task SignUserToTenant(Guid userId, Guid tenantId)
    {
        var user = await authRepository.SingleOrDefault(x => x.Id == userId)
            ?? throw new EntityNotFoundException(Resources.NoUsersForSpecifiedKeyWereFound);

        user.TenantId = tenantId;
        await authRepository.SaveChangesAsync();
    }

    public async Task UpdateUser(UserUpdateModel updateModel)
    {
        var isPasswordChanged = updateModel.Password != null;

        var user = await SingleOrDefault(x => x.Id == updateModel.Id)
            ?? throw new EntityNotFoundException();

        user = Mapper.MapOnlyUpdatedProperties(updateModel, user);

        if (isPasswordChanged)
        {
            user.Password = passwordHasher.HashPassword(updateModel.Password!);
        }

        await Repository.Update(user);
    }
}