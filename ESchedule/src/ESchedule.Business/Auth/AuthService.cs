﻿using ESchedule.Api.Models.Requests.Create.Users;
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
using Microsoft.IdentityModel.Tokens;
using PowerInfrastructure.AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
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
    IAuthRepository authRepository
)
    : BaseService<UserModel>(repository, mapper), IAuthService
{
    private readonly JwtSettings _jwtSettings = config.GetSection("Jwt").Get<JwtSettings>()!;

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
        ArgumentNullException.ThrowIfNull(userModel);

        ValidateEmail(userModel.Login);

        if (await IsLoginAlreadyRegistered(userModel.Login))
        {
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
        var userResult = await authRepository.FirstOrDefault(x => x.Password.ToLower() == key.ToLower());

        if (userResult.IsEmailConfirmed)
        {
            throw new Exception(Resources.TheUsersEmailIsAlreadyConfirmed);
        }

        userResult.IsEmailConfirmed = true;
        var userUpdateModel = Mapper.Map<UserUpdateModel>(userResult);

        await UpdateItem(userUpdateModel);

        return userResult.Id;
    }

    public async Task<UserModel?> GetAuthenticatedUserInfo(IEnumerable<Claim>? claims)
    {
        if (claims.IsNullOrEmpty())
        {
            return null!; //TODO throw 401
        }

        var userId = claims!.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var hasTenant = claims!.Any(x => x.Type == ClaimTypes.Surname);

        if (!Guid.TryParse(userId, out var id))
        {
            throw new InvalidOperationException("Invalid token provided");
        }

        var repo = hasTenant ? Repository : Repository.IgnoreQueryFilters();
        return await repo.SingleOrDefault(x => x.Id == id);
    }

    private async Task<UserModel> ValidateCredentials(AuthModel authModel)
    {
        ValidateEmail(authModel.Login);

        var user = await authRepository.FirstOrDefault(x => x.Login == authModel.Login);

        if (!user.IsEmailConfirmed)
        {
            throw new AuthenticationException(Resources.EmailConfirmationIsNeeded);
        }

        if (!passwordHasher.ComparePasswords(authModel.Password, user.Password))
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
       => await authRepository.Any(x => x.Login == login);
}