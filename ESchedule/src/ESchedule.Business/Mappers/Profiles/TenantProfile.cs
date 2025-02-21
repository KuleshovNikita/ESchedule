using AutoMapper;
using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
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