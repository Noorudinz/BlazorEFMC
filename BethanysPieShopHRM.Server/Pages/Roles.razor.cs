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
    public partial class Roles : ComponentBase
    {
        [Inject]
        public IRolesDataService RolesDataService { get; set; }

        public List<RolesVM> RoleList { get; set; }

        protected AddEditRoles AddEditRoleDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
           
           RoleList = (await RolesDataService.GetAllRoles()).ToList();         
          
        }

        public async void AddEditRolesDialog_OnDialogClose()
        {
            RoleList = (await RolesDataService.GetAllRoles()).ToList();
            StateHasChanged();
        }

        //Add
        protected void AddEditRoles()
        {
            AddEditRoleDialog.Show();
        }

        //Edit 
        protected void AddEditRoles(string Id)
        {
            AddEditRoleDialog.ShowEdit(Id);
        }    
        
        //deleter
        protected void DeleteRoles(string Id)
        {
            AddEditRoleDialog.ShowDelete(Id);
        }

    }
}
