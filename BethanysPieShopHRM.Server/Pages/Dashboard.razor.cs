using BethanysPieShopHRM.Server.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Pages
{
    public partial class Dashboard : ComponentBase
    {
        [Inject]
        public IAuthService AuthDataService { get; set; }
        public int UserCount { get; set; }
        protected override async Task OnInitializedAsync()
        {
            UserCount = (await AuthDataService.GetAllUsers()).Count();           

        }
    }
}
