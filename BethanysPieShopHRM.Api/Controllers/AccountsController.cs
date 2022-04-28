using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        //private static UserModel LoggedOutUser = new UserModel { IsAuthenticated = false };

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtConfig _jwtConfig;
        public AccountsController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtConfig = optionsMonitor.CurrentValue;

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterModel model)
        {
            var newUser = new IdentityUser {Id = model.Id, UserName = model.UserName, Email = model.Email };
            var result = new IdentityResult();

            if(model.Id != string.Empty && model.Id != null)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                user.UserName = newUser.UserName;
                user.Email = newUser.Email;               
                result = await _userManager.UpdateAsync(user);
            }               
            else            
                result = await _userManager.CreateAsync(newUser, model.Password);
            
          

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return Ok(new RegisterResult { Successful = false, Errors = errors });
              
            }

            return Ok(new RegisterResult { Successful = true });

        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {

            try
            {
                var model = new List<UserModel>();
                var users = _userManager.Users;

                var targetList = users.Select(x => new UserModel() { Id = x.Id, Name = x.UserName, Email = x.Email }).ToList();

                return Ok(targetList);
            }
            catch(Exception ex)
            {
                return null;
            }


        }

        [HttpGet]
        [Route("GetUserRolesById/{userId}")]
        public async Task<IActionResult> GetUserRolesById(string userId)
        {
            try
            {             
                var user = await _userManager.FindByIdAsync(userId);
                
               if(user == null)
                {
                    return null;
                }
                else
                {
                    var rolesSelection = new List<RolesSelection>();

                    var userRoleVM = new UserRoleVM();

                    userRoleVM.UserId = user.Id;
                    userRoleVM.UserName = user.UserName;
                    userRoleVM.UserEmail = user.Email;


                    foreach (var role in _roleManager.Roles)
                    {
                        var userRolesViewModel = new RolesSelection
                        {
                            RoleId = role.Id,
                            RoleName = role.Name
                        };

                        if (await _userManager.IsInRoleAsync(user, role.Name))
                        {
                            userRolesViewModel.IsSelected = true;
                        }
                        else
                        {
                            userRolesViewModel.IsSelected = false;
                        }

                        rolesSelection.Add(userRolesViewModel);
                    }
                    userRoleVM.RolesSelections = rolesSelection;

                    return Ok(userRoleVM);
                }            

            }
            catch (Exception ex)
            {
                return null;
            }


        }

        [HttpPost]
        [Route("RegisterUserRoles")]
        public async Task<IActionResult> RegisterUserRoles([FromBody] UserRoleVM model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return Ok(new RegisterResult { Successful = true, ErrorMsg = "User not found" });
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

         
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return Ok(new RegisterResult { Successful = false, Errors = errors });

            }

            result = await _userManager.AddToRolesAsync(user,
    model.RolesSelections.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return Ok(new RegisterResult { Successful = false, Errors = errors });
            }

            return Ok(new RegisterResult { Successful = true });

        }

        [HttpPost]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] RegisterModel model)
        {
            var user = new IdentityUser { Id = model.Id, UserName = model.UserName, Email = model.Email };             
            var result = new IdentityResult();

            var findUser = await _userManager.FindByIdAsync(user.Id);

            result = await _userManager.DeleteAsync(findUser);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return Ok(new RegisterResult { Successful = false, Errors = errors });

            }

            return Ok(new RegisterResult { Successful = true });

        }

        [HttpGet]
        [Route("GetEmail/{email}")]
        public async Task<IActionResult> GetEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);

                if (match.Success)
                {
                    var getUserByEmail = await _userManager.FindByEmailAsync(email);

                    if (getUserByEmail != null)
                    {
                        return Ok(new SendEmailResponse()
                        {
                            Message = "Email sent successfully !",
                            IsSend = true
                        });
                    }
                    else
                    {
                        return Ok(new SendEmailResponse()
                        {
                            Message = "Email not found !",
                            IsSend = false
                        });
                    }

                }
            }

            return Ok(new SendEmailResponse()
            {
                Message = "Invalid request!",
                IsSend = false
            });

        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            if (request != null)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user != null)
                {
                    var response = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.ConfirmPassword);

                    if (response.Succeeded)
                    {
                        return Ok(new ChangePasswordResponse()
                        {
                            Message = "Password changed successfully !",
                            IsChanges = true
                        });
                    }
                }
            }
            return Ok(new ChangePasswordResponse()
            {
                Message = "Invalid request !",
                IsChanges = false
            });

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {

            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser == null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                            "Invalid login request"
                        },
                        IsRegistered = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if (!isCorrect)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                            "Invalid login request"
                        },
                        IsRegistered = false
                    });
                }


                var role = await _userManager.GetRolesAsync(existingUser);

                var jwtToken = GenerateJwtToken(existingUser);

                return Ok(new RegistrationResponse()
                {
                    IsRegistered = true,
                    IdToken = jwtToken.IdToken,
                    ExpiresIn = jwtToken.ExpiresIn,
                    RefreshToken = jwtToken.RefreshToken,
                    Name = jwtToken.Name,
                    Email = jwtToken.Email,
                    LocalId = jwtToken.LocalId,
                    RolesName = role as List<string>
                });

                //return Ok(jwtToken);
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>() {
                    "Invalid payload"
                },
                IsRegistered = false
            });

        }

        private TokenResponse GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var response = new TokenResponse
            {
                LocalId = user.Id,
                IdToken = jwtTokenHandler.WriteToken(token),
                ExpiresIn = _jwtConfig.ExpiresIn,
                Email = user.Email,
                Name = user.UserName,
                RefreshToken = token.Id
            };

            return response;
        }
    }
}
