using BethanysPieShopHRM.Server.Repository;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace BethanysPieShopHRM.Server.Pages
{
    public class FlatOwnerEditBase : ComponentBase
    {
        [Inject]
        public IFlatOwner FlatOwnerDataService { get; set; }

        [Inject]
        public IBuilding BuildingDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string FlatId { get; set; }

        public InputText FirstNameInputText { get; set; }

        protected string BuildingId = string.Empty;

        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;
        public BethanysPieShopHRM.Shared.FlatOwner FlatOwner { get; set; } = new BethanysPieShopHRM.Shared.FlatOwner();

        public List<BethanysPieShopHRM.Shared.FlatOwner> FlatOwners { get; set; }
        public List<Building> Buildings { get; set; } = new List<Building>();
        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            Buildings = (await BuildingDataService.GetAllBuildings()).ToList();

            int.TryParse(FlatId, out var flatId);

            if (flatId == 0) 
            {
                //add some defaults
                FlatOwner = new BethanysPieShopHRM.Shared.FlatOwner { BuildingId = 1, PossesionDate = DateTime.Now };
            }
            else
            {
                FlatOwner = await FlatOwnerDataService.GetFlatOwner(int.Parse(FlatId));
            }

            BuildingId = FlatOwner.BuildingId.ToString();

        }

        protected async Task HandleValidSubmit()
        {
            FlatOwner.BuildingId = int.Parse(BuildingId);
    
            if (FlatOwner.FlatId == 0) //new
            {
                var addedFlatOwner = await FlatOwnerDataService.AddFlatOwner(FlatOwner);
                if (addedFlatOwner != null)
                {
                    StatusClass = "alert-success";
                    Message = "New flat owner added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new flat owner. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await FlatOwnerDataService.UpdateFlatOwner(FlatOwner);
                StatusClass = "alert-success";
                Message = "Flat Owner updated successfully.";
                Saved = true;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/flatowners");
        }
    }
}
