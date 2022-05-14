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
        public async Task<IActionResult> InvoiceByPeriods(string selectedDate)
        {
            var selectDate = Convert.ToDateTime(selectedDate);

            return Ok(_invoiceRepo.InvoiceByPeriods(selectDate));
        }

        [HttpGet]
        [Route("GetMailListToSendByPeriods/{selectedDate}")]
        public async Task<IActionResult> GetMailListToSendByPeriods(DateTime selectedDate)
        {
            return Ok(_invoiceRepo.GetMailListToSendByPeriods(selectedDate));
        }

        [HttpGet]
        [Route("InvoiceDetail/{billNo}")]
        public async Task<IActionResult> InvoiceDetail(Int64 billNo)
        {
            return Ok(_invoiceRepo.InvoiceDetails(billNo));
        }

        [HttpGet]
        [Route("GenerateBill/{selectedDate}")]
        public async Task<IActionResult> GenerateBill(DateTime selectedDate)
        {
            return Ok(_invoiceRepo.GenerateBill(selectedDate));
        }

        [HttpGet]
        [Route("SendMailGeneratedBills/{selectedDate}")]
        public async Task<IActionResult> SendMailGeneratedBills(DateTime selectedDate)
        {
            return Ok(_invoiceRepo.SendMailBill(selectedDate));
        }
    }
}
