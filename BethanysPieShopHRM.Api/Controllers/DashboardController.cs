using BethanysPieShopHRM.Api.Repository;
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
    public class DashboardController : ControllerBase
    {
        private readonly IDashboard _dashBoradRepository;

        public DashboardController(IDashboard dashBoradRepository)
        {
            _dashBoradRepository = dashBoradRepository;
        }

        [HttpGet]
        [Route("GetDashBoard")]
        public IActionResult GetDashBoradDetails()
        {
            return Ok(_dashBoradRepository.GetDashboardDetails());
        }
    }
}
