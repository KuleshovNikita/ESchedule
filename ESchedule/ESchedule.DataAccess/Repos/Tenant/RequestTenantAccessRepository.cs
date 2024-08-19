using ESchedule.DataAccess.Context;
using ESchedule.Domain.Tenant;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.Tenant
{
    public class RequestTenantAccessRepository : Repository<RequestTenantAccessModel>
    {
        public RequestTenantAccessRepository(TenantEScheduleDbContext context) : base(context)
        {
        }

        public override async Task<RequestTenantAccessModel> SingleOrDefault(Expression<Func<RequestTenantAccessModel, bool>> command)
            => await GetContext<RequestTenantAccessModel>()
                    .SingleOrDefaultAsync(command);
    }
}
