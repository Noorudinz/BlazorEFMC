using BethanysPieShopHRM.Server.Repository;
using Microsoft.AspNetCore.Components;
using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Pages.Invoice
{
    public class InvoiceBillBase : ComponentBase
    {
        [Inject]
        public IInvoice InvoiceDataService { get; set; }

        public Bills BillDetails { get; set; } = new Bills();

        [Parameter]
        public string BillNo { get; set; }

        protected override async Task OnInitializedAsync()
        {
            int.TryParse(BillNo, out var billNo);

            if(billNo != 0)
              BillDetails = await InvoiceDataService.InvoiceDetails(int.Parse(BillNo));

           
        }


    }
}
