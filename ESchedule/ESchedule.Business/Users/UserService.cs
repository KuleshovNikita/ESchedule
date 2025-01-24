using AutoMapper;
using ESchedule.Api.Models.Updates;
using ESchedule.Business.Extensions;
using ESchedule.Business.Mappers;
using ESchedule.Core.Interfaces;
using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.Auth;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;

namespace ESchedule.Business.Users
{
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
            var user = await _authRepository.SingleOrDefault(x => x.Id == userId);

            if (user == null)
            {
                throw new EntityNotFoundException(Resources.NoUsersForSpecifiedKeyWereFound);
            }

            user.TenantId = tenantId;
            await _authRepository.SaveChangesAsync();
        }

        public async Task UpdateUser(UserUpdateModel updateModel)
        {
            var isPsswordChanged = updateModel.Password != null;

            if (!await ItemExists(updateModel.Id))
            {
                throw new EntityNotFoundException();
            }

            var user = await FirstOrDefault(x => x.Id == updateModel.Id);
            user = _mapper.MapOnlyUpdatedProperties(updateModel, user);

            if(isPsswordChanged)
            {
                user.Password = _passwordHasher.HashPassword(updateModel.Password!);
            }

            await _repository.Update(user);
        }
    }
}
