using BethanysPieShopHRM.Server.Repository;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Pages.Payment
{
    public class ReceiptBase : ComponentBase
    {
        [Inject]
        public IPayment PaymentDataService { get; set; }

        public List<BethanysPieShopHRM.Shared.Receipt> GridData { get; set; }



        public void OnCommandClicked(CommandClickEventArgs<BethanysPieShopHRM.Shared.Receipt> args)
        {
            if (args.RowData != null)
            {
                if (args.CommandColumn.Type == CommandButtonType.Edit)
                {
                    //EditBuildingDialog.ShowEdit(args.RowData.BuildingId);
                }
            }
        }
    }
}
