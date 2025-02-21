using Microsoft.AspNetCore.Authorization;

namespace ESchedule.Domain.Policy.Requirements;

public class DispatcherRoleRequirement : IAuthorizationRequirement { }

public class TeacherRoleRequirement : IAuthorizationRequirement { }
