using AutoMapper;
using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business.Auth;
using ESchedule.Business.Users;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Tenant;
using Microsoft.EntityFrameworkCore;

namespace ESchedule.Business.Tenant
{
    public class TenantService : BaseService<TenantModel>, ITenantService
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IRepository<TenantSettingsModel> _tenantSettingsRepo;

        public TenantService(IRepository<TenantModel> repository,
            IRepository<TenantSettingsModel> settingsRepo,
            IAuthService authService, 
            IMapper mapper,
            IUserService userService) : base(repository, mapper)
        {
            _authService = authService;
            _tenantSettingsRepo = settingsRepo;
            _userService = userService;
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

            var authModel = new AuthModel
            {
                Login = request.Login,
                Password = request.Password,
            };

            var user = await _authService.ValidateCredentials(authModel);
            
            if(user.TenantId != null)
            {
                throw new InvalidOperationException(Resources.UserAlreadyBlongsToTenant);
            }
            var tenant = await CreateItem(request);

            var updateUser = new UserUpdateModel()
            {
                Id = user.Id,
                TenantId = tenant.Id,
                Role = Domain.Enums.Role.Dispatcher
            };

            await _userService.UpdateUser(updateUser);

            return tenant;
        }

        public async Task<TenantSettingsModel> CreateTenantSettings(TenantSettingsModel request)
            => await _tenantSettingsRepo.Insert(request);
    }
}
