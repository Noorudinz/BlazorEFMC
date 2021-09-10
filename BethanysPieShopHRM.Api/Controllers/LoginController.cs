﻿using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginController(IConfiguration configuration,
                               SignInManager<IdentityUser> signInManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

                if (!result.Succeeded)
                    return BadRequest(new LoginResult { Successful = false, Error = "Username and password are invalid." });

                var user = await _signInManager.UserManager.FindByNameAsync(login.Email);
                var roles = await _signInManager.UserManager.GetRolesAsync(user);
                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, login.Email));

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

                var token = new JwtSecurityToken(
                    _configuration["JwtIssuer"],
                    _configuration["JwtAudience"],
                    claims,
                    expires: expiry,
                    signingCredentials: creds
                );

                return Ok(new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });

                //var tokenDescriptor = new SecurityTokenDescriptor
                //{
                //    Subject = new ClaimsIdentity(new[]
                //        {
                //            new Claim(ClaimTypes.Name, login.Email)                          
                //    }),

                //    Expires = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"])),
                //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"])), SecurityAlgorithms.HmacSha256Signature)
                //};

                //var tokenHandler = new JwtSecurityTokenHandler();
                //var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                //var token = tokenHandler.WriteToken(securityToken);

                //return Ok(new LoginResult { Successful = true, Token = token });

            }
            catch(Exception ex)
            {
                return Ok(new LoginResult { Successful = true, Error = ex.ToString() });
            }

          
        }
    }
}
