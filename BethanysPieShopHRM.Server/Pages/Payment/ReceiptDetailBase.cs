using BethanysPieShopHRM.Server.Repository;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Pages.Payment
{
    public class ReceiptDetailBase: ComponentBase
    {
        [Inject]
        public IPayment PaymentDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string flatNo { get; set; }
        public List<BethanysPieShopHRM.Shared.Receipt> ReceiptList { get; set; } = new List<BethanysPieShopHRM.Shared.Receipt>();
        protected override async Task OnInitializedAsync()
        {
            ReceiptList = await PaymentDataService.GetReceiptByReceiptNo(flatNo);

        }
        public void BackToList()
        {
            NavigationManager.NavigateTo("/receiptList");
        }
    }
}
