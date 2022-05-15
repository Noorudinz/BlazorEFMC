using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BethanysPieShopHRM.Server.Repository
{
    public interface IPayment
    {
        List<Receipt> GetReceiptList();
        List<Receipt> GetReceiptByReceiptNo(string flatNo);
        List<AccountSummary> GetLastSummaryDetail();
        List<AccountSummary> GetSummaryByFlatNo(string flatNo);
        CommonResponse AddReceipt(Receipt receipt);
        PriceFactor GetPriceFactor();
        CommonResponse UpdatePriceFactor(PriceFactor priceFactor);
    }
}
