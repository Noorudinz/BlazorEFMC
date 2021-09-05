using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public AccountsController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterModel model)
        {
            var newUser = new IdentityUser { UserName = model.UserName, Email = model.Email };

            var result = await _userManager.CreateAsync(newUser, model.Password);


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

    }
}
