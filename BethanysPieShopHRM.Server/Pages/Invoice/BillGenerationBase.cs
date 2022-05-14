using BethanysPieShopHRM.Server.Repository;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Calendars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;
using Syncfusion.Blazor.Notifications;

namespace BethanysPieShopHRM.Server.Pages.Invoice
{
    public class BillGenerationBase : ComponentBase
    {
        public DateTime? DateValue { get; set; }
        public DateTime MaxDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public SfToast ToastObj;
        public string ToastPosition = "Right";

        public List<ToastModel> Toast = new List<ToastModel>
        {
        new ToastModel{ Title = "Information!", Content="Bill already generated!.", CssClass="e-toast-info", Icon="e-info toast-icons" }
        };

        [Inject]
        public IInvoice InvoiceDataService { get; set; }
        public List<Bills> GridData { get; set; }
        public string dateValue { get; set; } = string.Empty;

        public bool ShowButtons;
        public async void ValueChangeHandler(ChangedEventArgs<DateTime?> args)
        {
            ShowButtons = false;
            dateValue = Convert.ToDateTime(args.Value).ToString("yyyy/MM/dd");
            GridData = (await InvoiceDataService.InvoiceByPeriods(dateValue)).ToList();

            if (GridData.Count == 0)
                ShowButtons = true;
            else            
               await this.ToastObj.Show(Toast[0]);            
        }

        protected override async Task OnInitializedAsync()
        {
            ShowButtons = false;
            var dt = DateTime.Now.ToString("yyyy/MM/dd");
            GridData = (await InvoiceDataService.InvoiceByPeriods(dt)).ToList();

            if (GridData.Count == 0)
                ShowButtons = true;
            else
                await this.ToastObj.Show(Toast[0]);

        }


        protected async Task GenerateBill()
        {
            GridData = (await InvoiceDataService.GenerateBills(dateValue)).ToList();
        }

    }
}
