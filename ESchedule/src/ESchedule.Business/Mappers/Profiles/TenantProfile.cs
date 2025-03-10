﻿using AutoMapper;
using ESchedule.Api.Models.Requests.Create.Tenants;
using ESchedule.Api.Models.Requests.Create.Tenants.RequestAccess;
using ESchedule.Api.Models.Requests.Create.Tenants.Settings;
using ESchedule.Api.Models.Requests.Update.Tenants;
using ESchedule.Api.Models.Requests.Update.Tenants.Settings;
using ESchedule.Domain.Tenant;

namespace ESchedule.Business.Mappers.Profiles;

public class TenantProfile : Profile
{
    public TenantProfile()
    {
        CreateMap<TenantUpdateModel, TenantModel>();
        CreateMap<TenantCreateModel, TenantModel>();
        CreateMap<TenantSettingsUpdateModel, TenantSettingsModel>();
        CreateMap<TenantSettingsCreateModel, TenantSettingsModel>();
        CreateMap<RequestTenantAccessCreateModel, RequestTenantAccessModel>();
    }
}