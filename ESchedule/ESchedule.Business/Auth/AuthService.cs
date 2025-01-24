using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business.Email;
using ESchedule.Business.Mappers;
using ESchedule.Business.Users;
using ESchedule.Core.Interfaces;
using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.Auth;
using ESchedule.Domain;
using ESchedule.Domain.Auth;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace ESchedule.Business.Auth;

public class AuthService : BaseService<UserModel>, IAuthService
{
    private readonly IEmailService _emailService;
    private readonly IUserService _userService;
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly JwtSettings _jwtSettings;
    
    public AuthService(
        IRepository<UserModel> repository, 
        IMainMapper mapper, 
        IPasswordHasher passwordHasher, 
        IEmailService emailService, 
        IConfiguration config,
        IUserService userService,
        IAuthRepository authRepository) : base(repository, mapper)
    {
        _emailService = emailService;
        _passwordHasher = passwordHasher;
        _userService = userService;
        _authRepository = authRepository;

        _jwtSettings = config.GetSection("Jwt").Get<JwtSettings>()!;
    }

    public async Task<string> Login(AuthModel authModel)
    {
        if (authModel is null)
        {
            throw new ArgumentNullException(nameof(authModel), Resources.InvalidDataFoundCantAuthenticateUser);
        }

        var user = await ValidateCredentials(authModel);

        return BuildClaimsWithEmail(user);
    }

    public async Task Register(UserCreateModel userModel)
    {
        if (userModel is null)
        {
            throw new ArgumentNullException(Resources.InvalidDataFoundCantRegisterUser);
        }

        ValidateEmail(userModel.Login);

        if (await IsLoginAlreadyRegistered(userModel.Login))
        {
            throw new InvalidOperationException(Resources.TheLoginIsAlreadyRegistered);
        }

        var userDomainModel = _mapper.Map<UserModel>(userModel);        

        var hashedPassword = _passwordHasher.HashPassword(userDomainModel.Password);
        userDomainModel.Password = hashedPassword;
        userDomainModel.Id = Guid.NewGuid();

        await _repository.Insert(userDomainModel);
        await _emailService.SendConfirmEmailMessage(userDomainModel);
    }

    public async Task<Guid> ConfirmEmail(string key)
    {
        var userResult = await _authRepository.FirstOrDefault(x => x.Password.ToLower() == key.ToLower());

        if (userResult.IsEmailConfirmed)
        {
            throw new Exception(Resources.TheUsersEmailIsAlreadyConfirmed);
        }

        userResult.IsEmailConfirmed = true;
        var userUpdateModel = _mapper.Map<UserUpdateModel>(userResult);

        await UpdateItem(userUpdateModel);

        return userResult.Id;
    }

    private async Task<UserModel> ValidateCredentials(AuthModel authModel)
    {
        ValidateEmail(authModel.Login);

        var user = await _authRepository.FirstOrDefault(x => x.Login == authModel.Login);

        if (!user.IsEmailConfirmed)
        {
            throw new AuthenticationException(Resources.EmailConfirmationIsNeeded);
        }

        if (!_passwordHasher.ComparePasswords(authModel.Password, user.Password))
        {
            throw new AuthenticationException(Resources.WrongPasswordOrLogin);
        }

        return user;
    }

    private void ValidateEmail(string login)
    {
        if (!MailAddress.TryCreate(login, out _))
        {
            throw new FormatException(Resources.InvalidEmailAddressFormatSpecified);
        }
    }

    private string BuildClaimsWithEmail(UserModel userModel)
    {
        var claims = ClaimsSets.GetClaims(userModel);
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

    private async Task<bool> IsLoginAlreadyRegistered(string login)
       => await _authRepository.Any(x => x.Login == login);

    public async Task<UserModel> GetUserInfoWithTenant(Guid id)
        => await _userService.SingleOrDefault(x => x.Id == id);

    public async Task<UserModel> GetUserInfoWithoutTenant(Guid id)
        => await _authRepository.SingleOrDefault(x => x.Id == id);
}
