﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NetCoreCommon.Dtos;
using NetCoreCommon.HelperModels;
using NetCoreData.DbModels;

namespace NetCore3._1Angular9.Controllers
{
    [Route("api/[auth]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IPasswordHasher<ApplicationUser> _hasher;
        private readonly IConfigurationRoot _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager,
            ILogger<AuthController> logger,
            IPasswordHasher<ApplicationUser> hasher,
            IConfigurationRoot config,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _logger = logger;
            _hasher = hasher;
            _config = config;
            _signInManager = signInManager;
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredentialmodelDto model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    if (user != null)
                    {
                        var tokenPacket = CreateToken(user);
                        if (tokenPacket != null && tokenPacket.Result.Token != null)
                        {
                            return Ok(tokenPacket);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Loggin yapılırken hata oluştu: {ex}");

            }
            return BadRequest("Loggin başarılı olamadı lütfen bilgilerinizi kontrol ediniz.");
        }

        public async Task<IActionResult> Register([FromBody] RegisterModelDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Parametre Hatalı");

            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                    return BadRequest("Bu kullanıcı zaten var ");
                else
                {
                    user = new ApplicationUser
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        UserName = model.UserName
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                        return Ok(CreateToken(user));
                    else
                        return BadRequest(result.Errors);
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"Kayıt Esnasında Exception Hatası Alındı {ex}");
                return BadRequest($"Yeni kullanıcı kaydı sırasında hata alındı : {ex}");
            }

        }

        [HttpPost("Token")]
        public async Task<IActionResult> CreateToken([FromBody] CredentialmodelDto model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                    {
                        return Ok(CreateToken(user));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"JWT Tanımlarken bir hata oluştu : {ex.Message.ToString()}");
            }

            return null;
        }


        /// <summary>
        /// Create Token Private Methods
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        private async Task<JwtTokenPacket> CreateToken(ApplicationUser appUser)
        {
            var userClaims = await _userManager.GetClaimsAsync(appUser);

            var claims = new[]
             {
                new Claim(JwtRegisteredClaimNames.Sub,appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            }.Union(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));//appsettings.json içindeki token içindeki key getir.
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Token:Issuer"],
                audience: _config["Token:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: cred);

            return new JwtTokenPacket
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo.ToString(),
                UserName = appUser.UserName
            };
        }
    }
}