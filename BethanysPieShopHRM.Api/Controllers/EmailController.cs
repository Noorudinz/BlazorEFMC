using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Api.Repository;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmail _emailRepo;

        public EmailController(IEmail emailRepo)
        {
            _emailRepo = emailRepo;
        }

        [HttpGet]
        [Route("GetEmail")]
        public async Task<IActionResult> GetEmail()
        {
            return Ok(_emailRepo.GetEmail());
        }

        [HttpPost]
        [Route("UpdateEmailSetting")]
        public async Task<IActionResult> UpdateEmailSetting(Email email)
        {
            return Ok(_emailRepo.UpdateEmailSetting(email));
        }

        [HttpGet]
        [Route("GetEmailById/{Id}")]
        public async Task<IActionResult> GetEmailById(int Id)
        {
            return Ok(_emailRepo.GetEmailById(Id));
        }
    }
}
