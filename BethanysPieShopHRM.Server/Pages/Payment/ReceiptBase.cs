using BethanysPieShopHRM.Server.Components;
using BethanysPieShopHRM.Server.Repository;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Pages.Payment
{
    public class ReceiptBase : ComponentBase
    {
        [Inject]
        public IPayment PaymentDataService { get; set; }

        public List<BethanysPieShopHRM.Shared.Receipt> GridData { get; set; }

        protected EditReceiptDialogBase EditReceiptgDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            GridData = await PaymentDataService.GetReceiptList();
        }

        protected void EditReceipt()
        {
            EditReceiptgDialog.ShowEdit();
        }

        public async void EditReceipt_OnDialogClose()
        {
            GridData = (await PaymentDataService.GetReceiptList()).ToList();
            StateHasChanged();
        }

        public void OnCommandClicked(CommandClickEventArgs<BethanysPieShopHRM.Shared.Receipt> args)
        {
            if (args.RowData != null)
            {
                if (args.CommandColumn.Type == CommandButtonType.Edit)
                {
                    //EditBuildingDialog.ShowEdit(args.RowData.BuildingId);
                }
            }
        }
    }
}
