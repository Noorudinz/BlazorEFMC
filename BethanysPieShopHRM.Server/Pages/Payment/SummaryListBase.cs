using BethanysPieShopHRM.Server.Repository;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Pages.Payment
{
    public class SummaryListBase: ComponentBase
    {
        [Inject]
        public IPayment PaymentDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<BethanysPieShopHRM.Shared.AccountSummary> GridData { get; set; }

        protected override async Task OnInitializedAsync()
        {
            GridData = await PaymentDataService.GetLastSummaryDetail();
        }

        public void OnCommandClicked(CommandClickEventArgs<BethanysPieShopHRM.Shared.AccountSummary> args)
        {
            if (args.RowData != null)
            {
                if (args.CommandColumn.Type == CommandButtonType.Edit)
                {
                    NavigationManager.NavigateTo("/summaryDetail/" + args.RowData.FlatNo);
                }
            }
        }
    }
}
