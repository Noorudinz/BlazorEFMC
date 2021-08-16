using BethanysPieShopHRM.Server.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Components
{
    public class _EditCompanyDetailBase : ComponentBase
    {
        public bool ShowDialog { get; set; }

        public Company Company { get; set; } = new Company();

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        [Inject]
        public ICompanyDataService CompanyDataService { get; set; }

        public string OrgId { get; set; } = "1";
        //protected override async Task OnInitializedAsync()
        //{            

        //    Company = await CompanyDataService.GetCompanyDetails(int.Parse(OrgId));         
        //}

        public async void Show()
        {
            ResetDialog();
            ShowDialog = true;
            Company = await CompanyDataService.GetCompanyDetails(int.Parse(OrgId));
            StateHasChanged();
        }

        public async void ResetDialog()
        {
            Company = await CompanyDataService.GetCompanyDetails(int.Parse(OrgId));
            //Company = new Company();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {
            if(Company != null)
            {
                Company.updated_Date = DateTime.Now;
                await CompanyDataService.UpdateCompany(Company);
                ShowDialog = false;
            }
           

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
