using BethanysPieShopHRM.Server.Repository;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Pages.Invoice
{
    public class BillGenerationBase: ComponentBase
    {
        public DateTime? DateValue { get; set; }
        public DateTime MaxDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        [Inject]
        public IInvoice InvoiceDataService { get; set; }
        public List<BethanysPieShopHRM.Shared.Bills> GridData { get; set; }
        protected async Task FindOutBill()
        {
            DateTime dt = Convert.ToDateTime(DateValue);
            GridData = (await InvoiceDataService.InvoiceByPeriods(dt)).ToList();
        }
        protected async Task GenerateBill()
        {

        }
    }
}
