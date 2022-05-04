using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using BethanysPieShopHRM.Shared;
using BethanysPieShopHRM.Server.Repository;

namespace BethanysPieShopHRM.Server.Components
{
    public class AddBuildingDialogBase: ComponentBase
    {
        public bool ShowDialog { get; set; }

        public Building Building { get; set; } = new Building { };

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        [Inject]
        public IBuilding BuildingDataService { get; set; }



        public void Show()
        {
            ResetDialog();
            ShowDialog = true;
            StateHasChanged();
        }

        private void ResetDialog()
        {
            Building = new Building { };
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {
            await BuildingDataService.AddBuilding(Building);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }

        public async void ShowEdit(int Id)
        {
            Building = (await BuildingDataService.GetBuilding(Id));

            ShowDialog = true;
            StateHasChanged();

        }
    }
}
