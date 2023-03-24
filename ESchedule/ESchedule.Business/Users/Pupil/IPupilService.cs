using ESchedule.Api.Models.Updates;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using System.Linq.Expressions;

namespace ESchedule.Business.Users.Pupil
{
    public interface IPupilService
    {
        Task<ServiceResult<Empty>> AddPupil(PupilModel userModel);

        Task<ServiceResult<PupilModel>> GetPupil(Expression<Func<PupilModel, bool>> predicate);

        Task<ServiceResult<Empty>> UpdateUser(UserUpdateRequestModel userModel, Guid userId);

        Task<ServiceResult<Empty>> RemovePupil(Guid userId);
    }
}
