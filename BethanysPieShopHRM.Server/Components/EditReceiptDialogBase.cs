using BethanysPieShopHRM.Server.Repository;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Components
{
    public class EditReceiptDialogBase : ComponentBase
    {
        public bool ShowDialog { get; set; }
        public string StatusClass { get; set; } = string.Empty;
        public string ErrorMsg { get; set; } = string.Empty;

        public Receipt receipt { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        [Inject]
        public IPayment PaymentDataService { get; set; }

        public async void ShowEdit()
        {
            ResetDialog();
            ShowDialog = true;
            StateHasChanged();

        }

        private void ResetDialog()
        {
            receipt = new Receipt();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {
            var response = await PaymentDataService.AddReceipt(receipt);

            if (response.IsUpdated)
            {
                ShowDialog = false;

                await CloseEventCallback.InvokeAsync(true);
                StateHasChanged();
            }
            else
            {
                StatusClass = "alert-danger";
                ErrorMsg = string.Join(",", response.Message.ToArray());

            }

        }
    }
}
