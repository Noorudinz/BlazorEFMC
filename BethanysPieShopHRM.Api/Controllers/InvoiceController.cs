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
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoice _invoiceRepo;
        public InvoiceController(IInvoice invoiceRepo)
        {
            _invoiceRepo = invoiceRepo;
        }

        [HttpGet]
        [Route("InvoiceByFlatNo/{flatNo}")]
        public async Task<IActionResult> InvoiceByFlatNo(string flatNo)
        {         
            return Ok(_invoiceRepo.InvoiceByFlatNo(flatNo));
        }

        [HttpGet]
        [Route("InvoiceByFlatNo")]
        public async Task<IActionResult> InvoiceByFlatNo()
        {
            return Ok(_invoiceRepo.InvoiceByFlatNo());
        }

        [HttpGet]
        [Route("InvoiceByBillNo/{billNo}")]
        public async Task<IActionResult> InvoiceByBillNo(Int64 billNo)
        {
            return Ok(_invoiceRepo.InvoiceByBillNo(billNo));
        }

        [HttpGet]
        [Route("InvoiceByPeriods/{selectedDate}")]
        public async Task<IActionResult> InvoiceByPeriods(DateTime selectedDate)
        {
          return Ok(_invoiceRepo.InvoiceByPeriods(selectedDate));
        }

        [HttpGet]
        [Route("GetMailListToSendByPeriods/{selectedDate}")]
        public async Task<IActionResult> GetMailListToSendByPeriods(DateTime selectedDate)
        {
            return Ok(_invoiceRepo.GetMailListToSendByPeriods(selectedDate));
        }
    }
}
