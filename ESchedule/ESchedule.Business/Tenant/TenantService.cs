using AutoMapper;
using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business.Auth;
using ESchedule.Business.Users;
using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.Auth;
using ESchedule.Domain;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Tenant;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ESchedule.Business.Tenant
{
    public class TenantService : BaseService<TenantModel>, ITenantService
    {
        private readonly IAuthRepository _authRepo;
        private readonly IUserService _userService;
        private readonly IRepository<TenantSettingsModel> _tenantSettingsRepo;
        private readonly IRepository<RequestTenantAccessModel> _tenantRequestRepo;
        private readonly IHttpContextAccessor _httpAccessor;

        public TenantService(IRepository<TenantModel> repository,
            IRepository<TenantSettingsModel> settingsRepo,
            IRepository<RequestTenantAccessModel> tenantRequestRepo,
            IAuthRepository authService, 
            IMapper mapper,
            IUserService userService,
            IHttpContextAccessor httpAccessor) : base(repository, mapper)
        {
            _authRepo = authService;
            _tenantSettingsRepo = settingsRepo;
            _userService = userService;
            _httpAccessor = httpAccessor;
            _tenantRequestRepo = tenantRequestRepo;
        }

        public async Task<TenantModel> CreateTenant(TenantCreateModel request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var tenantExists = await _repository.SingleOrDefault(x => EF.Functions.Like(x.Name, $"{request.Name}"));

            if(tenantExists != null)
            {
                throw new InvalidOperationException(Resources.TenantAlreadyExists);
            }

            var userId = _httpAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            var user = await _authRepo.SingleOrDefault(x => x.Id == Guid.Parse(userId));
            
            if(user.TenantId != null)
            {
                throw new InvalidOperationException(Resources.UserAlreadyBlongsToTenant);
            }
            var tenant = await CreateItem(request);

            await _userService.SignUserToTenant(user.Id, tenant.Id);

            return tenant;
        }

        public async Task RequestTenantAccess(RequestTenantAccessCreateModel request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var tenantExists = await _repository.Any(x => x.Id == request.TenantId);

            if(!tenantExists)
            {
                throw new EntityNotFoundException("Tenant does not exist");
            }

            var entity = await _tenantRequestRepo.SingleOrDefault(x => x.UserId == request.UserId && x.TenantId == request.TenantId);

            if(entity != null)
            {
                throw new InvalidOperationException("A request to the tenant is already sent");
            }

            var domainModel = _mapper.Map<RequestTenantAccessModel>(request);

            await _tenantRequestRepo.Insert(domainModel);
        }

        public async Task<TenantSettingsModel> CreateTenantSettings(TenantSettingsModel request)
            => await _tenantSettingsRepo.Insert(request);
    }
}
