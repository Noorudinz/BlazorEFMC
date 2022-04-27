﻿using BethanysPieShopHRM.Api.Repository;
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
    public class PaymentsController : ControllerBase
    {
        private readonly IPayment _paymentRepo;

        public PaymentsController(IPayment paymentRepo)
        {
            _paymentRepo = paymentRepo;
        }

        [HttpGet]
        [Route("GetReceiptList")]
        public async Task<IActionResult> GetReceiptList()
        {
            return Ok(_paymentRepo.GetReceiptList());
        }

        [HttpGet]
        [Route("GetReceiptByReceiptNo/{flatNo}")]
        public async Task<IActionResult> GetReceiptByReceiptNo(string flatNo)
        {
            return Ok(_paymentRepo.GetReceiptByReceiptNo(flatNo));
        }

        [HttpGet]
        [Route("GetLastSummaryDetail")]
        public async Task<IActionResult> GetLastSummaryDetail()
        {
            return Ok(_paymentRepo.GetLastSummaryDetail());
        }

        [HttpGet]
        [Route("GetSummaryByFlatNo/{flatNo}")]
        public async Task<IActionResult> GetSummaryByFlatNo(string flatNo)
        {
            return Ok(_paymentRepo.GetSummaryByFlatNo(flatNo));
        }
    }
}
