using ESchedule.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
