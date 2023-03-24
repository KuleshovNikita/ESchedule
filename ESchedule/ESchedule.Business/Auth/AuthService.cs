using AutoMapper;
using ESchedule.Business.Email;
using ESchedule.Business.Hashing;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain;
using ESchedule.Domain.Auth;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace ESchedule.Business.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IEmailService _emailService;
        private readonly IRepository<UserCredentialsModel> _credentialsRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IMapper mapper, IPasswordHasher passwordHasher, IEmailService emailService,
            IRepository<UserCredentialsModel> credentialsRepo, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _emailService = emailService;
            _credentialsRepo = credentialsRepo;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

            _jwtSettings = config.GetSection("Jwt").Get<JwtSettings>()!;
        }

        public async Task<ServiceResult<string>> Login(UserCredentialsModel authModel)
        {
            var serviceResult = new ServiceResult<string>();

            if (authModel is null)
            {
                return serviceResult.FailAndThrow(Resources.InvalidDataFoundCantAuthenticateUser);
            }

            ValidateEmail(authModel.Login, serviceResult);

            var userResult = await _credentialsRepo.FirstOrDefault(x => x.Login == authModel.Login);

            if (!userResult.Value.IsEmailConfirmed)
            {
                return serviceResult.FailAndThrow(Resources.EmailConfirmationIsNeeded);
            }

            if (!_passwordHasher.ComparePasswords(authModel.Password, userResult.Value.Password))
            {
                return serviceResult.FailAndThrow(Resources.WrongPasswordOrLogin);
            }

            serviceResult.Value = BuildClaimsWithEmail(userResult.Value);

            return serviceResult.CatchAny();
        }

        public async Task<ServiceResult<string>> Register(UserCredentialsModel credentialsModel)
        {
            var serviceResult = new ServiceResult<string>();

            if (credentialsModel is null)
            {
                return serviceResult.FailAndThrow(Resources.InvalidDataFoundCantRegisterUser);
            }

            ValidateEmail(credentialsModel.Login, serviceResult);

            await _credentialsRepo.Insert(credentialsModel);
            await _emailService.SendEmailConfirmMessage(credentialsModel);

            serviceResult.Value = BuildInitialClaims(credentialsModel);

            return serviceResult.CatchAny();
        }

        public async Task<ServiceResult<string>> ConfirmEmail(string key)
        {
            var serviceResult = new ServiceResult<string>();

            var credentialsResult = await _credentialsRepo.FirstOrDefault(x => x.Password.ToLower() == key.ToLower());
            var credentialsModel = credentialsResult.Value;

            if (credentialsModel.IsEmailConfirmed)
            {
                return serviceResult.FailAndThrow(Resources.TheUsersEmailIsAlreadyConfirmed);
            }

            credentialsModel.IsEmailConfirmed = true;

            await _credentialsRepo.Update(credentialsModel);

            serviceResult.Value = BuildClaimsWithEmail(credentialsModel);
            return serviceResult.CatchAny();
        }

        private void ValidateEmail(string login, ServiceResult<string> serviceResult)
        {
            if (!MailAddress.TryCreate(login, out _))
            {
                serviceResult.FailAndThrow(Resources.InvalidEmailAddressFormatSpecified);
            }
        }

        private string BuildInitialClaims(UserCredentialsModel credentialsModel)
        {
            var claims = ClaimsSets.GetInitialClaims(credentialsModel);
            return BuildClaims(claims);
        }

        private string BuildClaimsWithEmail(UserCredentialsModel credentialsModel)
        {
            var claims = ClaimsSets.GetClaimsWithEmail(credentialsModel);
            return BuildClaims(claims);
        }

        private string BuildClaims(IEnumerable<Claim> claims)
        {
            var secretBytes = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var signingCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiresInMinutes),
                signingCredentials: signingCreds
            );

            var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenHandler;
        }
    }
}
