﻿using AutoMapper;
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

        public async Task<ServiceResult<string>> Login(AuthModel authModel)
        {
            var serviceResult = new ServiceResult<string>();

            if (authModel is null)
            {
                return serviceResult.FailAndThrow(Resources.InvalidDataFoundCantAuthenticateUser);
            }

            ValidateEmail(authModel.Login, serviceResult);

            var userResult = await _userService.First(x => x.Login == authModel.Login);

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

        public async Task<ServiceResult<Empty>> Register(UserCreateModel userModel)
        {
            var serviceResult = new ServiceResult<Empty>();

            if (userModel is null)
            {
                return serviceResult.FailAndThrow(Resources.InvalidDataFoundCantRegisterUser);
            }

            var userDomainModel = _mapper.Map<UserModel>(userModel);

            ValidateEmail(userModel.Login, serviceResult);

            await _userService.AddUser(userDomainModel);
            await _emailService.SendEmailConfirmMessage(userDomainModel);

            return serviceResult.CatchAny();
        }

        public async Task<ServiceResult<string>> ConfirmEmail(string key)
        {
            var serviceResult = new ServiceResult<string>();

            var userResult = await _userService.First(x => x.Password.ToLower() == key.ToLower());
            var userDomainModel = userResult.Value;

            if (userDomainModel.IsEmailConfirmed)
            {
                return serviceResult.FailAndThrow(Resources.TheUsersEmailIsAlreadyConfirmed);
            }

            userDomainModel.IsEmailConfirmed = true;
            var userUpdateModel = _mapper.Map<UserUpdateModel>(userDomainModel);

            await _userService.UpdateItem(userUpdateModel);

            serviceResult.Value = BuildClaimsWithEmail(userDomainModel);
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
