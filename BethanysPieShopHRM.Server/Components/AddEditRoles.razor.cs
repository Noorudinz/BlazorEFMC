using BethanysPieShopHRM.Server.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Components
{
    public partial class AddEditRoles : ComponentBase
    {
        public bool ShowDialog { get; set; }

        public string StatusClass { get; set; } = string.Empty;
        public string ErrorMsg { get; set; } = string.Empty;

        public RolesVM role { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        [Inject]
        public IRolesDataService RoleDataService { get; set; }



        public void Show()
        {
            ResetDialog();
            ShowDialog = true;
            StateHasChanged();
        }

        public async void ShowEdit(string Id)
        {
            //ResetDialog();
            role = await RoleDataService.GetRoleDetails(Id);
            ShowDialog = true;
            StateHasChanged();

        }
        private void ResetDialog()
        {
            role = new RolesVM();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {           
            var response = await RoleDataService.AddRole(role);

            if (response.IsSuccess)
            {
                ShowDialog = false;

                await CloseEventCallback.InvokeAsync(true);
                StateHasChanged();
            }
            else
            {
                StatusClass = "alert-danger";
                ErrorMsg = string.Join(",", response.Errors.ToArray());

            }
            
           
        }

    }
}
