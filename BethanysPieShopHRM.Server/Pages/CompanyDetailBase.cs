using BethanysPieShopHRM.ComponentsLibrary.Map;
using BethanysPieShopHRM.Server.Components;
using BethanysPieShopHRM.Server.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Pages
{
    public class CompanyDetailBase : ComponentBase
    {
        [Inject]
        public ICompanyDataService CompanyDataService { get; set; }
        public string OrgId { get; set; } = "1";

        public Company Company { get; set; } = new Company();

        public List<Marker> MapMarkers { get; set; } = new List<Marker>();

        protected _EditCompanyDetailBase _EditCompanyDetailDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //Company = (await CompanyDataService.GetCompany()).ToList();

            Company = await CompanyDataService.GetCompanyDetails(int.Parse(OrgId));

            MapMarkers = new List<Marker>
            {
                new Marker{Description = $"ELITE EFMC",  ShowPopup = false, X = 50.5592238711436, Y =  26.230605552338126}
            };
        }

        public async void EditCompany_OnDialogClose()
        {
            //Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
            //StateHasChanged();
        }

        protected void EditCompanyDialog()
        {
            _EditCompanyDetailDialog.Show();
        }

    }
}
