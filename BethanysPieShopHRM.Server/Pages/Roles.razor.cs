using BethanysPieShopHRM.Server.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Pages
{
    public partial class Roles : ComponentBase
    {
        [Inject]
        public IRolesDataService RolesDataService { get; set; }

        public List<Roles> RoleList { get; set; }

        //protected AddEmployeeDialogBase AddEmployeeDialog { get; set; }

        //protected override async Task OnInitializedAsync()
        //{
        //    RoleList = (await EmployeeDataService.GetAllEmployees()).ToList();
        //}

        //public async void AddEmployeeDialog_OnDialogClose()
        //{
        //    Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
        //    StateHasChanged();
        //}

        //protected void QuickAddEmployee()
        //{
        //    AddEmployeeDialog.Show();
        //}
    }
}
