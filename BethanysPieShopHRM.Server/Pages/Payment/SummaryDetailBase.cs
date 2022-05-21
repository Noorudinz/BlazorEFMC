using BethanysPieShopHRM.Server.Repository;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Pages.Payment
{
    public class SummaryDetailBase: ComponentBase
    {
        [Inject]
        public IPayment PaymentDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string flatNo { get; set; }
        public decimal? amount { get; set; }
        public List<BethanysPieShopHRM.Shared.AccountSummary> SummaryDetailList { get; set; } = new List<BethanysPieShopHRM.Shared.AccountSummary>();
        protected override async Task OnInitializedAsync()
        {
            SummaryDetailList = await PaymentDataService.GetSummaryByFlatNo(flatNo);
            amount = SummaryDetailList.Sum(a => a.Amount);
        }
        public void BackToList()
        {
            NavigationManager.NavigateTo("/summaryList");
        }
    }
}
