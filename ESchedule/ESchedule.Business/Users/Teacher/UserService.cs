using AutoMapper;
using ESchedule.Api.Models.Updates;
using ESchedule.Business.Extensions;
using ESchedule.Business.Hashing;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using System.Linq.Expressions;

namespace ESchedule.Business.Users.Teacher
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, IPasswordHasher passwordHasher, IUserRepository repository)
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _repository = repository;
        }

        public async Task<ServiceResult<Empty>> AddUser(BaseUserModel userModel)
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

        public async Task<ServiceResult<Empty>> RemoveUser(Guid userId)
        {
            var serviceResult = new ServiceResult<Empty>();

            if (await UserExists(userId))
            {
                var userModel = await GetUser(x => x.Id == userId);
                (await _repository.Remove(userModel.Value)).CatchAny();

                return serviceResult.Success();
            }

            return serviceResult.FailAndThrow(Resources.TheItemDoesntExist);
        }

        public async Task<ServiceResult<BaseUserModel>> GetUser(Expression<Func<BaseUserModel, bool>> predicate)
        {
            var result = await _repository.FirstOrDefault(predicate);

            return result.Catch<EntityNotFoundException>(Resources.TheItemDoesntExist)
                         .CatchAny();
        }

        public async Task<ServiceResult<Empty>> UpdateUser(UserUpdateRequestModel userModel, Guid userId)
        {
            userModel.Id = userId;
            var user = await GetUser(x => x.Id == userModel.Id);
            user.Value = _mapper.MapOnlyUpdatedProperties(userModel, user.Value);

            var result = await _repository.Update(user.Value);
            return result.CatchAny();
        }

        private async Task<bool> UserExists(Guid userId)
        {
            var result = await _repository.Any(x => x.Id == userId);
            return result.CatchAny().Value;
        }

        private async Task<bool> IsLoginAlreadyRegistered(string login)
        {
            var result = await _repository.Any(x => x.Login == login);
            return result.CatchAny().Value;
        }
    }
}
