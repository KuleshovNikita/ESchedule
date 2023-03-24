using ESchedule.Api.Models.Updates;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using System.Linq.Expressions;

namespace ESchedule.Business.Users.Teacher
{
    public interface IUserService
    {
        Task<ServiceResult<Empty>> AddUser(BaseUserModel userModel);

        Task<ServiceResult<BaseUserModel>> GetUser(Expression<Func<BaseUserModel, bool>> predicate);

        Task<ServiceResult<Empty>> UpdateUser(UserUpdateRequestModel userModel, Guid userId);

        Task<ServiceResult<Empty>> RemoveUser(Guid userId);
    }
}
