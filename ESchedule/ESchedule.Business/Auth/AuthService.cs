using AutoMapper;
using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business.Email;
using ESchedule.Business.Users;
using ESchedule.Core.Interfaces;
using ESchedule.Domain;
using ESchedule.Domain.Auth;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
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
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IMapper mapper, IPasswordHasher passwordHasher, IEmailService emailService,
            IUserService userService, IConfiguration config)
        {
            _emailService = emailService;
            _userService = userService;
            _passwordHasher = passwordHasher;
            _mapper = mapper;

            _jwtSettings = config.GetSection("Jwt").Get<JwtSettings>()!;
        }

        public async Task<string> Login(AuthModel authModel)
        {
            var serviceResult = new ServiceResult<string>();

            if (authModel is null)
            {
                throw new Exception(Resources.InvalidDataFoundCantAuthenticateUser);
            }

            ValidateEmail(authModel.Login, serviceResult);

            var user = await _userService.FirstNew(x => x.Login == authModel.Login);

            if (!user.IsEmailConfirmed)
            {
                throw new Exception(Resources.EmailConfirmationIsNeeded);
            }

            if (!_passwordHasher.ComparePasswords(authModel.Password, user.Password))
            {
                throw new Exception(Resources.WrongPasswordOrLogin);
            }

            return BuildClaimsWithEmail(user);
        }

        public async Task<ServiceResult<Empty>> Register(UserCreateModel userModel)
        {
            var serviceResult = new ServiceResult<Empty>();

            if (userModel is null)
            {
                return serviceResult.FailAndThrow(Resources.InvalidDataFoundCantRegisterUser);
            }

            var logins = await _userService.GetItems(x => x.Login == userModel.Login);

            if (logins.Value.Any())
            {
                return serviceResult.FailAndThrow(Resources.UserWithSuchEmailAlreadyRegistered);
            }

            var userDomainModel = _mapper.Map<UserModel>(userModel);

            ValidateEmail(userModel.Login, serviceResult);

            await _userService.AddUser(userDomainModel);
            await _emailService.SendEmailConfirmMessage(userDomainModel);

            return serviceResult.CatchAny();
        }

        public async Task<ServiceResult<Guid>> ConfirmEmail(string key)
        {
            var serviceResult = new ServiceResult<Guid>();

            var userResult = await _userService.First(x => x.Password.ToLower() == key.ToLower());
            var userDomainModel = userResult.Value;

            if (userDomainModel.IsEmailConfirmed)
            {
                return serviceResult.FailAndThrow(Resources.TheUsersEmailIsAlreadyConfirmed);
            }

            userDomainModel.IsEmailConfirmed = true;
            var userUpdateModel = _mapper.Map<UserUpdateModel>(userDomainModel);

            await _userService.UpdateItem(userUpdateModel);

            serviceResult.Value = userDomainModel.Id;// BuildClaimsWithEmail(userDomainModel);
            return serviceResult.CatchAny();
        }

        private void ValidateEmail<TType>(string login, ServiceResult<TType> serviceResult)
        {
            if (!MailAddress.TryCreate(login, out _))
            {
                serviceResult.FailAndThrow(Resources.InvalidEmailAddressFormatSpecified);
            }
        }

        private string BuildClaimsWithEmail(UserModel userModel)
        {
            var claims = ClaimsSets.GetClaimsWithEmail(userModel);
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
