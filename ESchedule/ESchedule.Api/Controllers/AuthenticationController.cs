﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ESchedule.Business.Auth;
using ESchedule.Domain;
using ESchedule.ServiceResulting;
using ESchedule.Api.Models.Requests;

namespace ESchedule.Api.Controllers
{
    public class AuthenticationController : ResultingController
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService) => _authService = authService;

        [HttpPost("register")]
        public async Task<ServiceResult<string>> Register([FromBody] UserRequestModel registerModel)
            => await RunWithServiceResult(async () => await _authService.Register(registerModel));

        [HttpPost("login")]
        public async Task<ServiceResult<string>> Login([FromBody] AuthModel authModel)
            => await RunWithServiceResult(async () => await _authService.Login(authModel));

        [HttpPatch("confirmEmail/{key}")]
        public async Task<ServiceResult<string>> ConfirmEmail(string key)
            => await RunWithServiceResult(async () =>
            {
                key = Uri.UnescapeDataString(key);
                return await _authService.ConfirmEmail(key);
            });

        [Authorize]
        [HttpGet("logout")]
        public async Task<ServiceResult<Empty>> LogOut()
            => await RunWithServiceResult(async () =>
            {
                await HttpContext.SignOutAsync();

                return SuccessEmptyResult();
            });
    }
}