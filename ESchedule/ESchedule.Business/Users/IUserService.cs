using ESchedule.Api.Models.Updates;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;

namespace ESchedule.Business.Users
{
    public interface IUserService : IBaseService<UserModel>
    {
        Task AddUser(UserModel userModel);

        Task UpdateUser(UserUpdateModel updateModel);

        Task SignUserToTenant(Guid userId);
    }
}