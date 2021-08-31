using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {           
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEditRole([FromBody] RolesVM rolevm)
        {
            if (rolevm == null)
                return BadRequest();

            if (rolevm.Name == string.Empty)
            {
                ModelState.AddModelError("Role", "The role name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            IdentityRole idRole = new IdentityRole();
         
           // idRole.ConcurrencyStamp = DateTime.Now.ToString();

            if(rolevm.Id == null)
            {
                idRole.Name = rolevm.Name.ToLower();
                idRole.NormalizedName = rolevm.Name.ToUpper();

                IdentityResult result = await _roleManager.CreateAsync(idRole);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(x => x.Description);

                    return Ok(new RolesResultVM { IsSuccess = false, Errors = errors });

                }

                return Ok(new RolesResultVM { IsSuccess = true });
            }
            else
            {
                var role = await _roleManager.FindByIdAsync(rolevm.Id);

                role.Name = rolevm.Name.ToLower();
                role.NormalizedName = rolevm.Name.ToUpper();

                IdentityResult result = await _roleManager.UpdateAsync(role);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(x => x.Description);

                    return Ok(new RolesResultVM { IsSuccess = false, Errors = errors });

                }

                return Ok(new RolesResultVM { IsSuccess = true });
            }

        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
       
            return Ok(roles);
        }

        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRoleById(string roleId)
        {
            return Ok(await _roleManager.FindByIdAsync(roleId));
        }

        [HttpPost]
        [Route("DeleteRole")]
        public async Task<IActionResult> DeleteRole([FromBody] RolesVM rolevm)
        {
            if (rolevm.Id == null || rolevm.Id == string.Empty)
                return BadRequest();

            var role = new IdentityRole();

            role.Id = rolevm.Id;
            role.Name = rolevm.Name;
            role.NormalizedName = rolevm.NormalizedName;
            role.ConcurrencyStamp = rolevm.ConcurrencyStamp;            

            IdentityResult result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return Ok(new RolesResultVM { IsSuccess = false, Errors = errors });

            }

            return Ok(new RolesResultVM { IsSuccess = true });
        }
    }
}
