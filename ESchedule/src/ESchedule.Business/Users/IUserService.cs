using ESchedule.Api.Models.Updates;
using ESchedule.Domain.Users;

namespace ESchedule.Business.Users;

public interface IUserService : IBaseService<UserModel>
{
    Task UpdateUser(UserUpdateModel updateModel);

    Task SignUserToTenant(Guid userId);

    Task SignUserToTenant(Guid userId, Guid tenantId);
}