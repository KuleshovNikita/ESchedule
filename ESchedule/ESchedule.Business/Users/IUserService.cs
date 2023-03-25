using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;

namespace ESchedule.Business.Users
{
    public interface IUserService : IBaseService<UserModel>
    {
        Task<ServiceResult<Empty>> AddUser(UserModel userModel);
    }
}
