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
    public class BillGenerationController : ControllerBase
    {
        private readonly IBillGeneration _billGenRepo;
        public BillGenerationController(IBillGeneration billGenRepo)
        {
            _billGenRepo = billGenRepo;
        }


        [HttpGet]
        [Route("SendMailGeneratedBills/{selectedDate}")]
        public async Task<IActionResult> SendMailGeneratedBills(DateTime selectedDate)
        {
            return Ok(_billGenRepo.SendMailGeneratedBills(selectedDate));
        }

        [HttpGet]
        [Route("GenerateBill/{selectedDate}")]
        public async Task<IActionResult> GenerateBill(DateTime selectedDate)
        {
            return Ok(_billGenRepo.GenerateBill(selectedDate));
        }
    }
}
