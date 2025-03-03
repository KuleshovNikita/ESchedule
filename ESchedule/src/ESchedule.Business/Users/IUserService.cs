using ESchedule.Api.Models.Requests.Update.Users;
using ESchedule.Domain.Users;

namespace ESchedule.Business.Users;

public interface IUserService : IBaseService<UserModel>
{
    Task<UserModel> UpdateUser(UserUpdateModel updateModel);

    Task SignUserToTenant(Guid userId);

    Task SignUserToTenant(Guid userId, Guid tenantId);
}