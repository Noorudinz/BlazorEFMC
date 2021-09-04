using BethanysPieShopHRM.Server.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Pages
{
    
    public partial class UsersRoles : ComponentBase
    {
        [Inject]
        public IAuthService AuthDataService { get; set; }
        public List<UserModelList> GridData { get; set; }
        //protected async override void OnInitialized()
        //{
        //    IEnumerable<UserModel> data = (await AuthDataService.GetAllUsers());
        //    GridData = data.ToList();
        //}

        public List<UserModelList> Orders { get; set; }

        protected async override void OnInitialized()
        {
            IEnumerable<UserModel> data = (await AuthDataService.GetAllUsers());
            GridData = data.Select(x => new UserModelList() { Name = x.Name, Email = x.Email, IsAuthenticated = x.IsAuthenticated }).ToList();

            //Orders = Enumerable.Range(1, 75).Select(x => new Order()
            //{
            //    OrderID = 1000 + x,
            //    CustomerID = (new string[] { "ALFKI", "ANANTR", "ANTON", "BLONP", "BOLID" })[new Random().Next(5)],
            //    Freight = 2.1 * x,
            //    OrderDate = DateTime.Now.AddDays(-x),
            //}).ToList();
        }

        public class UserModelList
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public bool IsAuthenticated { get; set; }
        }

    }

}
