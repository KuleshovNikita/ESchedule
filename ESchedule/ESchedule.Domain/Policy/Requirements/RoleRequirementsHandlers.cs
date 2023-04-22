using ESchedule.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ESchedule.Domain.Policy.Requirements
{
    public class DispatcherRoleHandler : AuthorizationHandler<DispatcherRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DispatcherRoleRequirement requirement)
            => new RequirementsBase().VerifyRole(context, requirement, Role.Dispatcher);
    }

    public class TeacherRoleHandler : AuthorizationHandler<TeacherRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TeacherRoleRequirement requirement)
            => new RequirementsBase().VerifyRole(context, requirement, Role.Teacher);
    }
}
