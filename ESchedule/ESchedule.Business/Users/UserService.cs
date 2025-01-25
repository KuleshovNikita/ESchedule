using ESchedule.Api.Models.Updates;
using ESchedule.Business.Mappers;
using ESchedule.Core.Interfaces;
using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.Auth;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;

namespace ESchedule.Business.Users;

public class UserService : BaseService<UserModel>, IUserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthRepository _authRepository;
    private readonly ITenantContextProvider _tenantContextProvider;

    public UserService(IRepository<UserModel> repository, IMainMapper mapper, IPasswordHasher passwordHasher,
         ITenantContextProvider tenantContextProvider, IAuthRepository authRepository) 
        : base(repository, mapper)
    {
        _passwordHasher = passwordHasher;
        _tenantContextProvider = tenantContextProvider;
        _authRepository = authRepository;
    }

    public async Task SignUserToTenant(Guid userId)
        => await SignUserToTenant(userId, _tenantContextProvider.Current.TenantId);

    public async Task SignUserToTenant(Guid userId, Guid tenantId)
    {
        var user = await _authRepository.SingleOrDefault(x => x.Id == userId)
            ?? throw new EntityNotFoundException(Resources.NoUsersForSpecifiedKeyWereFound);

        user.TenantId = tenantId;
        await _authRepository.SaveChangesAsync();
    }

    public async Task UpdateUser(UserUpdateModel updateModel)
    {
        var isPasswordChanged = updateModel.Password != null;

        var user = await SingleOrDefault(x => x.Id == updateModel.Id)
            ?? throw new EntityNotFoundException();

        user = _mapper.MapOnlyUpdatedProperties(updateModel, user);

        if (isPasswordChanged)
        {
            user.Password = _passwordHasher.HashPassword(updateModel.Password!);
        }

        await _repository.Update(user);
    }
}
