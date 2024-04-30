using AutoMapper;
using ESchedule.Api.Models.Updates;
using ESchedule.Business.Extensions;
using ESchedule.Core.Interfaces;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;

namespace ESchedule.Business.Users
{
    public class UserService : BaseService<UserModel>, IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITenantContextProvider _tenantContextProvider;

        public UserService(IRepository<UserModel> repository, IMapper mapper, IPasswordHasher passwordHasher,
             ITenantContextProvider tenantContextProvider) 
            : base(repository, mapper)
        {
            _passwordHasher = passwordHasher;
            _tenantContextProvider = tenantContextProvider;
        }

        public async Task AddUser(UserModel userModel)
        {
            if (await IsLoginAlreadyRegistered(userModel.Login))
            {
                throw new InvalidOperationException(Resources.TheLoginIsAlreadyRegistered);
            }

            var hashedPassword = _passwordHasher.HashPassword(userModel.Password);
            userModel.Password = hashedPassword;
            userModel.Id = Guid.NewGuid();

            await _repository.Insert(userModel);
        }

        public async Task SignUserToTenant(Guid userId)
        {
            var user = await SingleOrDefault(x => x.Id == userId);

            if(user == null)
            {
                throw new EntityNotFoundException(Resources.NoUsersForSpecifiedKeyWereFound);
            }

            user.TenantId = _tenantContextProvider.Current.TenantId;
            await _repository.SaveChangesAsync();
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

        private async Task<bool> IsLoginAlreadyRegistered(string login)
            => await _repository.Any(x => x.Login == login);
    }
}
