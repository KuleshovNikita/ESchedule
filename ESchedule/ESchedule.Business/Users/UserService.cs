using AutoMapper;
using ESchedule.Api.Models.Updates;
using ESchedule.Business.Extensions;
using ESchedule.Core.Interfaces;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;

namespace ESchedule.Business.Users
{
    public class UserService : BaseService<UserModel>, IUserService
    {
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IRepository<UserModel> repository, IMapper mapper, IPasswordHasher passwordHasher) 
            : base(repository, mapper)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task<ServiceResult<Empty>> AddUser(UserModel userModel)
        {
            var serviceResult = new ServiceResult<Empty>();

            if (await IsLoginAlreadyRegistered(userModel.Login))
            {
                return serviceResult.FailAndThrow(Resources.TheLoginIsAlreadyRegistered);
            }

            var hashedPassword = _passwordHasher.HashPassword(userModel.Password);
            userModel.Password = hashedPassword;
            userModel.Id = Guid.NewGuid();

            (await _repository.Insert(userModel)).CatchAny();

            return serviceResult.Success();
        }

        public async Task<ServiceResult<Empty>> UpdateUser(UserUpdateModel updateModel)
        {
            var serviceResult = new ServiceResult<Empty>();

            if (!await ItemExists(updateModel.Id))
            {
                return serviceResult.FailAndThrow(Resources.TheItemDoesntExist);
            }

            var user = await First(x => x.Id == updateModel.Id);
            user.Value = _mapper.MapOnlyUpdatedProperties(updateModel, user.Value);

            user.Value.Password = _passwordHasher.HashPassword(user.Value.Password);

            var result = await _repository.Update(user.Value);
            return result.Success();
        }

        private async Task<bool> IsLoginAlreadyRegistered(string login)
        {
            var result = await _repository.Any(x => x.Login == login);
            return result.CatchAny().Value;
        }
    }
}
