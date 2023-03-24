using ESchedule.Api.Models.Updates;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using System.Linq.Expressions;

namespace ESchedule.Business.Users
{
    public interface IUserService
    {
        Task<ServiceResult<Empty>> AddUser(UserModel userModel);

        Task<ServiceResult<UserModel>> GetUser(Expression<Func<UserModel, bool>> predicate);

        Task<ServiceResult<Empty>> UpdateUser(UserUpdateRequestModel userModel, Guid userId);

        Task<ServiceResult<Empty>> RemoveUser(Guid userId);
    }
}
