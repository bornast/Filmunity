﻿using Application.Dtos.User;
using Application.Interfaces;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ActionFilters;
using Application.Dtos.Common;
using Application.Interfaces.Common;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAuthValidatorService _authValidatorService;

        public AuthController(IAuthService authService, IAuthValidatorService authValidatorService)
        {
            _authService = authService;
            _authValidatorService = authValidatorService;
        }        

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLogin)
        {
            _authValidatorService.ValidateForLogin(userForLogin);

            var token = await _authService.Login(userForLogin);

            return Ok(token);            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegistrationDto userForRegistration)
        {
            await _authValidatorService.ValidateForRegistration(userForRegistration);

            await _authService.Register(userForRegistration);

            return Ok();
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken(TokenDto tokenForRefresh)
        {
            await _authValidatorService.ValidateBeforeTokenRefresh(tokenForRefresh);

            var token = await _authService.RefreshToken(tokenForRefresh);

            return Ok(token);
        }

        [HttpPost("loginWithFacebook")]
        public async Task<IActionResult> LoginWithFacebook(FacebookLoginDto facebookLogin)
        {
            await _authValidatorService.ValidateForLoginWithFacebook(facebookLogin);

            var token = await _authService.LoginWithFacebook(facebookLogin);

            return Ok(token);
        }

        [HttpGet("getTwitterRequestToken")]
        public IActionResult GetTwitterRequestToken()
        {
            var requestToken = _authService.GetTwitterRequestToken();

            return Ok(requestToken);
        }

        [HttpPost("loginWithTwitter")]
        public async Task<IActionResult> LoginWithTwitter(TwitterLoginDto twitterLogin)
        {
            _authValidatorService.ValidateForLoginWithTwitter(twitterLogin);

            var token = await _authService.LoginWithTwitter(twitterLogin);

            return Ok(token);
        }
    }
}
