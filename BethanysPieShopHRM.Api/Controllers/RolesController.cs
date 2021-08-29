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
        public async Task<IActionResult> CreateRole([FromBody] RolesVM rolevm)
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
            idRole.Name = rolevm.Name.ToLower();
            idRole.NormalizedName = rolevm.Name.ToUpper();
            //idRole.ConcurrencyStamp = (DateTime.Now).ToString();

            IdentityResult result = await _roleManager.CreateAsync(idRole);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return Ok(new RolesResultVM { IsSuccess = false, Errors = errors });

            }

            return Ok(new RolesResultVM { IsSuccess = true });
           
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
       
            return Ok(roles);
        }
    }
}
