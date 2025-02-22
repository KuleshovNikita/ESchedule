using ESchedule.Api.Models.Requests.Create.Users;
using ESchedule.Api.Models.Requests.Update.Users;
using ESchedule.Business.Email;
using ESchedule.Business.Hashing;
using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.Auth;
using ESchedule.Domain;
using ESchedule.Domain.Auth;
using ESchedule.Domain.Properties;
using ESchedule.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PowerInfrastructure.AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace ESchedule.Business.Auth;

public class AuthService(
    IRepository<UserModel> repository,
    IMainMapper mapper,
    IPasswordHasher passwordHasher,
    IEmailService emailService,
    IConfiguration config,
    IAuthRepository authRepository,
    ILogger<AuthService> logger
)
    : BaseService<UserModel>(repository, mapper), IAuthService
{
    private readonly JwtSettings _jwtSettings = config.GetSection("Jwt").Get<JwtSettings>()!;

    public async Task<string> Login(AuthModel authModel)
    {
        logger.LogInformation("Trying to login user {email}", authModel.Login);

        if (authModel is null)
        {
            throw new ArgumentNullException(nameof(authModel), Resources.InvalidDataFoundCantAuthenticateUser);
        }

        var user = await ValidateCredentials(authModel);

        return BuildClaimsWithEmail(user);
    }

    public async Task Register(UserCreateModel userModel)
    {
        logger.LogTrace("Trying to login user");

        ArgumentNullException.ThrowIfNull(userModel);

        if (await IsLoginAlreadyRegistered(userModel.Login))
        {
            logger.LogWarning("User login is already registered: {login}", userModel.Login);
            throw new InvalidOperationException(Resources.TheLoginIsAlreadyRegistered);
        }

        var userDomainModel = Mapper.Map<UserModel>(userModel);

        var hashedPassword = passwordHasher.HashPassword(userDomainModel.Password);
        userDomainModel.Password = hashedPassword;
        userDomainModel.Id = Guid.NewGuid();

        await Repository.Insert(userDomainModel);
        await emailService.SendConfirmEmailMessage(userDomainModel);
    }

    public async Task<Guid> ConfirmEmail(string key)
    {
        logger.LogTrace("Trying to confirm email by key {key}", key);

        var userResult = await authRepository.FirstOrDefault(x => x.Password.ToLower() == key.ToLower());

        if (userResult.IsEmailConfirmed)
        {
            logger.LogWarning("Cannot confirm email for user {id} because it is already confirmed", userResult.Id);
            throw new Exception(Resources.TheUsersEmailIsAlreadyConfirmed);
        }

        userResult.IsEmailConfirmed = true;
        var userUpdateModel = Mapper.Map<UserUpdateModel>(userResult);

        await UpdateItem(userUpdateModel);

        logger.LogInformation("Email for user {id} confirmed successfully", userResult.Id);    

        return userResult.Id;
    }

    public async Task<UserModel?> GetAuthenticatedUserInfo(IEnumerable<Claim>? claims)
    {
        if (claims.IsNullOrEmpty())
        {
            logger.LogError("Cannot get authenticated user info, claims set is empty");
            return null!; //TODO throw 401
        }

        var userId = claims!.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var hasTenant = claims!.Any(x => x.Type == ClaimTypes.Surname);

        if (!Guid.TryParse(userId, out var id))
        {
            logger.LogError("User id is invalid - {id}", userId);
            throw new InvalidOperationException("Invalid token provided");
        }

        var repo = hasTenant ? Repository : Repository.IgnoreQueryFilters();
        return await repo.SingleOrDefault(x => x.Id == id);
    }

    private async Task<UserModel> ValidateCredentials(AuthModel authModel)
    {
        var user = await authRepository.FirstOrDefault(x => x.Login == authModel.Login);

        if (!user.IsEmailConfirmed)
        {
            logger.LogWarning("Cannot login user {id} because email is not confirmed", user.Id);
            throw new AuthenticationException(Resources.EmailConfirmationIsNeeded);
        }

        if (!passwordHasher.ComparePasswords(authModel.Password, user.Password))
        {
            logger.LogWarning("Cannot login user {id} because password was not correct", user.Id);
            throw new AuthenticationException(Resources.WrongPasswordOrLogin);
        }

        return user;
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
       => await authRepository.Any(x => x.Login == login);
}