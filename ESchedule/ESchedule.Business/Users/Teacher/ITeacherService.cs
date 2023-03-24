using ESchedule.Api.Models.Updates;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using System.Linq.Expressions;

namespace ESchedule.Business.Users.Teacher
{
    public interface ITeacherService
    {
        Task<ServiceResult<Empty>> AddTeacher(TeacherModel userModel);

        Task<ServiceResult<TeacherModel>> GetTeacher(Expression<Func<TeacherModel, bool>> predicate);

        Task<ServiceResult<Empty>> UpdateTeacher(UserUpdateRequestModel userModel, Guid userId);

        Task<ServiceResult<Empty>> RemoveTeacher(Guid userId);
    }
}
