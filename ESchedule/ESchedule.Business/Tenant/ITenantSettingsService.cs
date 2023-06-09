﻿using ESchedule.Domain.Tenant;
using ESchedule.ServiceResulting;

namespace ESchedule.Business.Tenant
{
    public interface ITenantSettingsService : IBaseService<TenantSettingsModel>
    {
        Task<ServiceResult<List<object>>> BuildSchedulesTimeTable(Guid tenantId);
    }
}
